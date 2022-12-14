namespace CleanArchitecture.Domain.Entities;

public class Quiz : BaseAuditableEntity
{
    public int Id { get; set; }

    public string Description { get; set; } = "";

    public bool Active { get; set; } = false;

    public IList<Question> Questions { get; set; } = new List<Question>();

    public IList<Result> Results { get; set; } = new List<Result>();
}
