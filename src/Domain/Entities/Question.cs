namespace CleanArchitecture.Domain.Entities;

public class Question : BaseAuditableEntity
{
    public string Description { get; set; } = null!;

    public int QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;

    public IList<Option> Options { get; set; } = new List<Option>();
}
