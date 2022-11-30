namespace CleanArchitecture.Domain.Events.Quiz;

public class QuizCreatedEvent : BaseEvent
{
    public QuizCreatedEvent(Entities.Quiz quiz)
    {
        Quiz = quiz;
    }

    public Entities.Quiz Quiz { get; }
}
