using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Events.Option;
using MediatR;

namespace CleanArchitecture.Application.Options.Commands.CreateOption;

public record CreateOptionCommand : IRequest<OptionDto>
{
    public int QuestionId { get; init; }

    public IList<CreateOptionSkillDto>? OptionSkills { get; set; }

    public string Description { get; init; } = null!;
}

public class CreateOptionCommandHandler : IRequestHandler<CreateOptionCommand, OptionDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateOptionCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OptionDto> Handle(CreateOptionCommand request, CancellationToken cancellationToken)
    {
        var entity = new Option
        {
            QuestionId = request.QuestionId,
            Description = request.Description,
        };

        if (request.OptionSkills != null)
        {
            IList<OptionSkill> skillList = new List<OptionSkill>();
            foreach (var item in request.OptionSkills)
            {
                OptionSkill optionSkill = new OptionSkill();
                optionSkill.SkillLevel = (SkillLevel)item.SkillLevel;
                optionSkill.OptionId = entity.Id;
                optionSkill.SkillId = item.SkillId;
                skillList.Add(optionSkill);
            }

            entity.OptionSkills = skillList;
        }

        entity.AddDomainEvent(new OptionCreatedEvent(entity));

        _context.Options.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<OptionDto>(entity);

        return result;
    }
}
