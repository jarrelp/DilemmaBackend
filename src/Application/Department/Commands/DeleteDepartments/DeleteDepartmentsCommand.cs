using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Department;
using MediatR;

namespace CleanArchitecture.Application.Departments.Commands.DeleteDepartments;

public record DeleteDepartmentsCommand() : IRequest<int[]>
{
    public int[] Ids { get; init; } = null!;
}

public class DeleteDepartmentsCommandHandler : IRequestHandler<DeleteDepartmentsCommand, int[]>
{
    private readonly IApplicationDbContext _context;

    public DeleteDepartmentsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int[]> Handle(DeleteDepartmentsCommand request, CancellationToken cancellationToken)
    {
        foreach(var item in request.Ids)
        {
            var entity = await _context.Departments
            .FindAsync(new object[] { item }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Department), item);
            }

            _context.Departments.RemoveRange(entity);

            entity.AddDomainEvent(new DepartmentDeletedEvent(entity));
        }

        await _context.SaveChangesAsync(cancellationToken);

        return request.Ids;
    }
}
