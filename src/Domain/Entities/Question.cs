namespace CleanArchitecture.Domain.Entities;

public class Question : BaseAuditableEntity
{
    public int Id { get; set; }

    public string Description { get; set; } = "";

    public int QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;

    public IList<Option> Options { get; set; } = new List<Option>();
}
