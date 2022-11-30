namespace CleanArchitecture.Domain.Events.Question;

public class QuestionCreatedEvent : BaseEvent
{
    public QuestionCreatedEvent(Entities.Question question)
    {
        Question = question;
    }

    public Entities.Question Question { get; }
}
