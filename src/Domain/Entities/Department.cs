namespace CleanArchitecture.Domain.Entities;

public class Department : BaseAuditableEntity
{
    public string Name { get; set; } = null!;

    public IList<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();
}
