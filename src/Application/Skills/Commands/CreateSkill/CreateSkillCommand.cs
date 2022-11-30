using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Skill;
using MediatR;

namespace CleanArchitecture.Application.Skills.Commands.CreateSkill;

public record CreateSkillCommand : IRequest<int>
{
    public string Name { get; init; } = null!;
}

public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateSkillCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var entity = new Skill
        {
            Name = request.Name
        };

        entity.AddDomainEvent(new SkillCreatedEvent(entity));

        _context.Skills.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
