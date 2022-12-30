namespace CleanArchitecture.Domain.Events.OptionSkill;

public class OptionSkillDeletedEvent : BaseEvent
{
    public OptionSkillDeletedEvent(Entities.OptionSkill option)
    {
        OptionSkill = option;
    }

    public Entities.OptionSkill OptionSkill { get; }
}
