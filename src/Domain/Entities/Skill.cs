namespace CleanArchitecture.Domain.Entities;

public class Skill : BaseAuditableEntity
{
    public string Name { get; set; } = null!;

    public IList<OptionSkill> OptionSkills { get; set; } = new List<OptionSkill>();
}
