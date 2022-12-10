namespace CleanArchitecture.Domain.Entities;

public class Department : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
}
