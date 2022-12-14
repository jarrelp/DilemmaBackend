namespace CleanArchitecture.Domain.Entities;

public class Option : BaseAuditableEntity
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;

    public Result? Result { get; set; }

    public IList<OptionSkill> OptionSkills { get; set; } = new List<OptionSkill>();
}
