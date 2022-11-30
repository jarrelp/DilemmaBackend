using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Departments.Commands.UpdateDepartment;

public record UpdateDepartmentCommand : IRequest
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;
}

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateDepartmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Departments
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Department), request.Id);
        }

        entity.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
