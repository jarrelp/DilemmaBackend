using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Security;
using MediatR;

namespace CleanArchitecture.Application.Skills.Commands.PurgeSkills;

//[Authorize(Roles = "Administrator")]
//[Authorize(Policy = "CanPurge")]
public record PurgeSkillsCommand : IRequest;

public class PurgeSkillsCommandHandler : IRequestHandler<PurgeSkillsCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgeSkillsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(PurgeSkillsCommand request, CancellationToken cancellationToken)
    {
        _context.Skills.RemoveRange(_context.Skills);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
