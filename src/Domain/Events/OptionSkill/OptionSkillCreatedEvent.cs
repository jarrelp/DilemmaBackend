namespace CleanArchitecture.Domain.Events.OptionSkill;

public class OptionSkillCreatedEvent : BaseEvent
{
    public OptionSkillCreatedEvent(Entities.OptionSkill option)
    {
        OptionSkill = option;
    }

    public Entities.OptionSkill OptionSkill { get; }
}
