using CleanArchitecture.Domain.Events.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Results.EventHandlers;

public class ResultCreatedEventHandler : INotificationHandler<ResultCreatedEvent>
{
    private readonly ILogger<ResultCreatedEventHandler> _logger;

    public ResultCreatedEventHandler(ILogger<ResultCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ResultCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
