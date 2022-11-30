namespace CleanArchitecture.Domain.Events.Skill;

public class SkillCreatedEvent : BaseEvent
{
    public SkillCreatedEvent(Entities.Skill skill)
    {
        Skill = skill;
    }

    public Entities.Skill Skill { get; }
}
