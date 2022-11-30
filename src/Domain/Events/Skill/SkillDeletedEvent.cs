namespace CleanArchitecture.Domain.Events.Skill;

public class SkillDeletedEvent : BaseEvent
{
    public SkillDeletedEvent(Entities.Skill skill)
    {
        Skill = skill;
    }

    public Entities.Skill Skill { get; }
}
