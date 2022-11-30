namespace CleanArchitecture.Domain.Events.Result;

public class ResultDeletedEvent : BaseEvent
{
    public ResultDeletedEvent(Entities.Result result)
    {
        Result = result;
    }

    public Entities.Result Result { get; }
}
