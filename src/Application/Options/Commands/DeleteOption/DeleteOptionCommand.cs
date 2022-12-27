using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Option;
using MediatR;

namespace CleanArchitecture.Application.Options.Commands.DeleteOption;

public record DeleteOptionCommand(int Id) : IRequest<int>;

public class DeleteOptionCommandHandler : IRequestHandler<DeleteOptionCommand, int>
{
    private readonly IApplicationDbContext _context;

    public DeleteOptionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(DeleteOptionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Options
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Option), request.Id);
        }

        _context.OptionSkills.RemoveRange(_context.OptionSkills.Where(x => x.OptionId == request.Id));

        _context.Options.Remove(entity);

        entity.AddDomainEvent(new OptionDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
