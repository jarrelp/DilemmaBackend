using CleanArchitecture.Domain.Events.Question;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Questions.EventHandlers;

public class QuestionCreatedEventHandler : INotificationHandler<QuestionCreatedEvent>
{
    private readonly ILogger<QuestionCreatedEventHandler> _logger;

    public QuestionCreatedEventHandler(ILogger<QuestionCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(QuestionCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
