namespace CleanArchitecture.Domain.Events.Department;

public class DepartmentCreatedEvent : BaseEvent
{
    public DepartmentCreatedEvent(Entities.Department department)
    {
        Department = department;
    }

    public Entities.Department Department { get; }
}
