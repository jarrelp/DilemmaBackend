namespace CleanArchitecture.Domain.Entities;

public class Result : BaseAuditableEntity
{
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;

    public string ApplicationUserId { get; set; } = "";
    public ApplicationUser ApplicationUser { get; set; } = null!;

    public IList<Option> Answers { get; set; } = new List<Option>();
}