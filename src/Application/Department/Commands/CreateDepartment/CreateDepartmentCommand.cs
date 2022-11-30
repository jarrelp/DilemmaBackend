using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Department;
using MediatR;

namespace CleanArchitecture.Application.Departments.Commands.CreateDepartment;

public record CreateDepartmentCommand : IRequest<int>
{
    public string Name { get; init; } = null!;
}

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateDepartmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = new Department
        {
            Name = request.Name
        };

        entity.AddDomainEvent(new DepartmentCreatedEvent(entity));

        _context.Departments.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
