using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.OptionSkill;
using MediatR;

namespace CleanArchitecture.Application.OptionSkills.Commands.DeleteOptionSkill;

public record DeleteOptionSkillCommand(string Id) : IRequest<string>;

public class DeleteOptionSkillCommandHandler : IRequestHandler<DeleteOptionSkillCommand, string>
{
    private readonly IApplicationDbContext _context;

    public DeleteOptionSkillCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(DeleteOptionSkillCommand request, CancellationToken cancellationToken)
    {
        string[] ids = request.Id.Split('-');

        var entity = _context.OptionSkills
            .Where(x => x.OptionId == Int32.Parse(ids[0]) && x.SkillId == Int32.Parse(ids[1])).FirstOrDefault();

        if (entity == null)
        {
            throw new NotFoundException(nameof(OptionSkill), request.Id);
        }

        _context.OptionSkills.Remove(entity);

        entity.AddDomainEvent(new OptionSkillDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return entity.OptionId + "-" + entity.SkillId;
    }
}
