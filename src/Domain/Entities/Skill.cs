namespace CleanArchitecture.Domain.Entities;

public class Skill : BaseAuditableEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public IList<OptionSkill> OptionSkills { get; set; } = new List<OptionSkill>();
}
