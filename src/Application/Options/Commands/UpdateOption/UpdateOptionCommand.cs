using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Options.Commands.CreateOption;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;

namespace CleanArchitecture.Application.Options.Commands.UpdateOption;

public record UpdateOptionCommand : IRequest
{
    public int Id { get; init; }

    public IList<CreateOptionSkillDto>? OptionSkills { get; init; }

    public string? Description { get; init; }
}

public class UpdateOptionCommandHandler : IRequestHandler<UpdateOptionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateOptionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateOptionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Options
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Option), request.Id);
        }

        _context.OptionSkills.RemoveRange(_context.OptionSkills.Where(x => x.OptionId == request.Id));

        if (request.OptionSkills != null)
        {
            IList<OptionSkill> skillList = new List<OptionSkill>();
            foreach (var item in request.OptionSkills)
            {
                var optionSkill = new OptionSkill
                {
                    SkillLevel = (SkillLevel)item.SkillLevel,
                    OptionId = entity.Id,
                    SkillId = item.SkillId
                };
                skillList.Add(optionSkill);
            }

            entity.OptionSkills = skillList;
            
            // hier moet ie pas gevalideert worden
        }

        if (request.Description != null)
            entity.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
