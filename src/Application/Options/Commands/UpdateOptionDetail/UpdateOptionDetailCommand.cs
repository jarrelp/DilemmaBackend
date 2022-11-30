using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Options.Commands.CreateOption;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;

namespace CleanArchitecture.Application.Options.Commands.UpdateOptionDetail;

public record UpdateOptionDetailCommand : IRequest
{
    public int Id { get; init; }

    public IList<CreateOptionSkillDto>? OptionSkills { get; init; }

    public string? Description { get; init; }
}

public class UpdateOptionDetailCommandHandler : IRequestHandler<UpdateOptionDetailCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateOptionDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateOptionDetailCommand request, CancellationToken cancellationToken)
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
                OptionSkill optionSkill = new OptionSkill();
                optionSkill.SkillLevel = (SkillLevel)item.SkillLevel;
                optionSkill.OptionId = entity.Id;
                optionSkill.SkillId = item.SkillId;
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
