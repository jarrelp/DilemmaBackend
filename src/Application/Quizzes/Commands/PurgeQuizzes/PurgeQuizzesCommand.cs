using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Security;
using MediatR;

namespace CleanArchitecture.Application.Quizzes.Commands.PurgeQuizzes;

//[Authorize(Roles = "Administrator")]
//[Authorize(Policy = "CanPurge")]
public record PurgeQuizzesCommand : IRequest;

public class PurgeQuizsCommandHandler : IRequestHandler<PurgeQuizzesCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgeQuizsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(PurgeQuizzesCommand request, CancellationToken cancellationToken)
    {
        _context.Quizzes.RemoveRange(_context.Quizzes);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
