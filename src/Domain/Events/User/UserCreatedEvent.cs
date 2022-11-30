namespace CleanArchitecture.Domain.Events.User;

public class UserCreatedEvent : BaseEvent
{
    public UserCreatedEvent(Entities.ApplicationUser user)
    {
        User = user;
    }

    public Entities.ApplicationUser User { get; }
}
