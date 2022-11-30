namespace CleanArchitecture.Domain.Events.Option;

public class OptionDeletedEvent : BaseEvent
{
    public OptionDeletedEvent(Entities.Option option)
    {
        Option = option;
    }

    public Entities.Option Option { get; }
}
