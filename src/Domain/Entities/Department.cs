namespace CleanArchitecture.Domain.Entities;

public class Department : BaseAuditableEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public IList<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();
}
