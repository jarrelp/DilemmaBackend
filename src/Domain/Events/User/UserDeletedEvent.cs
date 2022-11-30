namespace CleanArchitecture.Domain.Events.User;

public class UserDeletedEvent : BaseEvent
{
    public UserDeletedEvent(Entities.ApplicationUser user)
    {
        User = user;
    }

    public Entities.ApplicationUser User { get; }
}
