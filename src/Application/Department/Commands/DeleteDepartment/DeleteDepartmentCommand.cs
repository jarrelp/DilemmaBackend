using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Department;
using MediatR;

namespace CleanArchitecture.Application.Departments.Commands.DeleteDepartment;

public record DeleteDepartmentCommand(int Id) : IRequest<int>;

public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;

    public DeleteDepartmentCommandHandler(
        IApplicationDbContext context,
        IIdentityService identityService
        )
    {
        _context = context;
        _identityService = identityService;
    }

    public async Task<int> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Departments
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Department), request.Id);
        }

        foreach(var item in entity.ApplicationUsers)
        {
            await _identityService.DeleteUserDepartmentAsync(item.Id);
        }

        _context.Departments.Remove(entity);

        entity.AddDomainEvent(new DepartmentDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
