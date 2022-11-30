using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Result;
using MediatR;

namespace CleanArchitecture.Application.Results.Commands.DeleteResult;

public record DeleteResultCommand(int Id) : IRequest;

public class DeleteResultCommandHandler : IRequestHandler<DeleteResultCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteResultCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteResultCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Results
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Result), request.Id);
        }

        _context.Results.Remove(entity);

        entity.AddDomainEvent(new ResultDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
