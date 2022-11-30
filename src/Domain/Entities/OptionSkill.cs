namespace CleanArchitecture.Domain.Entities;

public class OptionSkill : BaseAuditableEntity
{
    public SkillLevel SkillLevel { get; set; }

    public int SkillId { get; set; }
    public Skill Skill { get; set; } = null!;

    public int OptionId { get; set; }
    public Option Option { get; set; } = null!;
}
