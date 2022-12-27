using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Skill;
using MediatR;

namespace CleanArchitecture.Application.Skills.Commands.DeleteSkill;

public record DeleteSkillCommand(int Id) : IRequest<int>;

public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, int>
{
    private readonly IApplicationDbContext _context;

    public DeleteSkillCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Skills
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Skill), request.Id);
        }

        _context.Skills.Remove(entity);

        entity.AddDomainEvent(new SkillDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
