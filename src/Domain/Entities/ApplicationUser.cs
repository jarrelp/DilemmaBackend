using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Domain.Entities;

public class ApplicationUser : ApplicationUserDomainEvent
{
    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }

    public IList<Result> Results { get; set; } = new List<Result>();
}
