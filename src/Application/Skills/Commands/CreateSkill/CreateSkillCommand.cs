using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Skill;
using MediatR;

namespace CleanArchitecture.Application.Skills.Commands.CreateSkill;

public record CreateSkillCommand : IRequest<SkillDto>
{
    public string Name { get; init; } = null!;
}

public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, SkillDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateSkillCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SkillDto> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var entity = new Skill
        {
            Name = request.Name
        };

        entity.AddDomainEvent(new SkillCreatedEvent(entity));

        _context.Skills.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<SkillDto>(entity);

        return result;
    }
}
