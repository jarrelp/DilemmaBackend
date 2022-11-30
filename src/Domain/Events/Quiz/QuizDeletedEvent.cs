namespace CleanArchitecture.Domain.Events.Quiz;

public class QuizDeletedEvent : BaseEvent
{
    public QuizDeletedEvent(Entities.Quiz quiz)
    {
        Quiz = quiz;
    }

    public Entities.Quiz Quiz { get; }
}
