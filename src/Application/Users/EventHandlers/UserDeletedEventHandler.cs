using CleanArchitecture.Domain.Events.User;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Users.EventHandlers;

public class UserDeletedEventHandler : INotificationHandler<UserDeletedEvent>
{
    private readonly ILogger<UserDeletedEventHandler> _logger;

    public UserDeletedEventHandler(ILogger<UserDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
