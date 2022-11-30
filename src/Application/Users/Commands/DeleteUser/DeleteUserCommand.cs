using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.User;
using MediatR;

namespace CleanArchitecture.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(string Id) : IRequest;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;

    public DeleteUserCommandHandler(
        IIdentityService identityService,
        IApplicationDbContext context
        )
    {
        _identityService = identityService;
        _context = context;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _identityService.GetUserAsync(request.Id);

        if (entity == null)
        {
            throw new NotFoundException(nameof(ApplicationUser), request.Id);
        }

        var result = await _identityService.DeleteUserAsync(request.Id);

        if (result.Succeeded)
        {
            var quizResults = await _identityService.GetUserResults(request.Id);
            if (quizResults.Count > 0)
            {
                _context.Results.RemoveRange(quizResults);
            }
        }

        entity.AddDomainEvent(new UserDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
