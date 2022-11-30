namespace CleanArchitecture.Domain.Events.Option;

public class OptionCreatedEvent : BaseEvent
{
    public OptionCreatedEvent(Entities.Option option)
    {
        Option = option;
    }

    public Entities.Option Option { get; }
}
