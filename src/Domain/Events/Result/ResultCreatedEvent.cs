namespace CleanArchitecture.Domain.Events.Result;

public class ResultCreatedEvent : BaseEvent
{
    public ResultCreatedEvent(Entities.Result result)
    {
        Result = result;
    }

    public Entities.Result Result { get; }
}
