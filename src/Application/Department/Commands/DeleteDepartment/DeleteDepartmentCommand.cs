using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Department;
using MediatR;

namespace CleanArchitecture.Application.Departments.Commands.DeleteDepartment;

public record DeleteDepartmentCommand(int Id) : IRequest;

public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteDepartmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Departments
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Department), request.Id);
        }

        _context.Departments.Remove(entity);

        entity.AddDomainEvent(new DepartmentDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
