using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Skills.Commands.UpdateSkill;

public record UpdateSkillCommand : IRequest
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;
}

public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateSkillCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Skills
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Skill), request.Id);
        }

        entity.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
