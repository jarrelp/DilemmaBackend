using CleanArchitecture.Domain.Events.Skill;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Skills.EventHandlers;

public class SkillCreatedEventHandler : INotificationHandler<SkillCreatedEvent>
{
    private readonly ILogger<SkillCreatedEventHandler> _logger;

    public SkillCreatedEventHandler(ILogger<SkillCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SkillCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
