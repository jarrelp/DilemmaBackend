namespace CleanArchitecture.Domain.Events.Question;

public class QuestionDeletedEvent : BaseEvent
{
    public QuestionDeletedEvent(Entities.Question question)
    {
        Question = question;
    }

    public Entities.Question Question { get; }
}
