using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Events.OptionSkill;
using MediatR;

namespace CleanArchitecture.Application.OptionSkills.Commands.CreateOptionSkill;

public record CreateOptionSkillCommand : IRequest<OptionSkillDto>
{
    public int OptionId { get; init; }

    public int SkillId { get; init; }

    public int SkillLevel { get; init; }
}

public class CreateOptionSkillCommandHandler : IRequestHandler<CreateOptionSkillCommand, OptionSkillDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateOptionSkillCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OptionSkillDto> Handle(CreateOptionSkillCommand request, CancellationToken cancellationToken)
    {
        var entity = new OptionSkill
        {
            OptionId = request.OptionId,
            SkillId = request.SkillId,
            SkillLevel = (SkillLevel)request.SkillLevel
        };

        entity.AddDomainEvent(new OptionSkillCreatedEvent(entity));

        _context.OptionSkills.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<OptionSkillDto>(entity);

        return result;
    }
}
