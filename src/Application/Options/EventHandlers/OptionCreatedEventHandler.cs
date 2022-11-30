using CleanArchitecture.Domain.Events.Option;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Options.EventHandlers;

public class OptionCreatedEventHandler : INotificationHandler<OptionCreatedEvent>
{
    private readonly ILogger<OptionCreatedEventHandler> _logger;

    public OptionCreatedEventHandler(ILogger<OptionCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(OptionCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
