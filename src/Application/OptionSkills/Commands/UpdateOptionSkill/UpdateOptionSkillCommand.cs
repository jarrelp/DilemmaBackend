using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.OptionSkills.Commands.CreateOptionSkill;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;

namespace CleanArchitecture.Application.OptionSkills.Commands.UpdateOptionSkill;

public record UpdateOptionSkillCommand : IRequest<OptionSkillDto>
{
    public string Id { get; init; } = null!;
    public int SkillLevel { get; init; }
}

public class UpdateOptionSkillCommandHandler : IRequestHandler<UpdateOptionSkillCommand, OptionSkillDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateOptionSkillCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OptionSkillDto> Handle(UpdateOptionSkillCommand request, CancellationToken cancellationToken)
    {
        string[] ids = request.Id.Split('-');

        var entity = _context.OptionSkills
            .Where(x => x.OptionId == Int32.Parse(ids[0]) && x.SkillId == Int32.Parse(ids[1])).FirstOrDefault();

        if (entity == null)
        {
            throw new NotFoundException(nameof(OptionSkill), request.Id);
        }

        entity.SkillLevel = (SkillLevel)request.SkillLevel;

        await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<OptionSkillDto>(entity);

        return result;
    }
}
