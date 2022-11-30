namespace CleanArchitecture.Domain.Events.Department;

public class DepartmentDeletedEvent : BaseEvent
{
    public DepartmentDeletedEvent(Entities.Department department)
    {
        Department = department;
    }

    public Entities.Department Department { get; }
}
