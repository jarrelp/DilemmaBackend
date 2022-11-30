using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Security;
using MediatR;

namespace CleanArchitecture.Application.Departments.Commands.PurgeDepartments;

//[Authorize(Roles = "Administrator")]
//[Authorize(Policy = "CanPurge")]
public record PurgeDepartmentsCommand : IRequest;

public class PurgeDepartmentsCommandHandler : IRequestHandler<PurgeDepartmentsCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgeDepartmentsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(PurgeDepartmentsCommand request, CancellationToken cancellationToken)
    {
        _context.Departments.RemoveRange(_context.Departments);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
