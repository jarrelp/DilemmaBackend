using CleanArchitecture.Domain.Events.Quiz;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Quizzes.EventHandlers;

public class QuizCreatedEventHandler : INotificationHandler<QuizCreatedEvent>
{
    private readonly ILogger<QuizCreatedEventHandler> _logger;

    public QuizCreatedEventHandler(ILogger<QuizCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(QuizCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
