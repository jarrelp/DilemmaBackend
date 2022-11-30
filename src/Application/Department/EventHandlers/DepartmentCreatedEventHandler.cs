using CleanArchitecture.Domain.Events.Department;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Departments.EventHandlers;

public class DepartmentCreatedEventHandler : INotificationHandler<DepartmentCreatedEvent>
{
    private readonly ILogger<DepartmentCreatedEventHandler> _logger;

    public DepartmentCreatedEventHandler(ILogger<DepartmentCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DepartmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
