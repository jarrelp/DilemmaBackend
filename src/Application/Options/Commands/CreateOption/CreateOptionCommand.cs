using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Events.Option;
using MediatR;

namespace CleanArchitecture.Application.Options.Commands.CreateOption;

public record CreateOptionCommand : IRequest<int>
{
    public int QuestionId { get; init; }

    public IList<CreateOptionSkillDto>? OptionSkills { get; set; }

    public string Description { get; init; } = null!;
}

public class CreateOptionCommandHandler : IRequestHandler<CreateOptionCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateOptionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateOptionCommand request, CancellationToken cancellationToken)
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

        return entity.Id;
    }
}
