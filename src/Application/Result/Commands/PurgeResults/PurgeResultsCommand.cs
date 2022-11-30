using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Security;
using MediatR;

namespace CleanArchitecture.Application.Results.Commands.PurgeResults;

//[Authorize(Roles = "Administrator")]
//[Authorize(Policy = "CanPurge")]
public record PurgeResultsCommand : IRequest;

public class PurgeResultsCommandHandler : IRequestHandler<PurgeResultsCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgeResultsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(PurgeResultsCommand request, CancellationToken cancellationToken)
    {
        _context.Results.RemoveRange(_context.Results);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
