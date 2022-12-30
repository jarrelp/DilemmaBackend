using CleanArchitecture.Domain.Events.OptionSkill;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.OptionSkills.EventHandlers;

public class OptionSkillCreatedEventHandler : INotificationHandler<OptionSkillCreatedEvent>
{
    private readonly ILogger<OptionSkillCreatedEventHandler> _logger;

    public OptionSkillCreatedEventHandler(ILogger<OptionSkillCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(OptionSkillCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
