using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Skills.Commands.UpdateSkill;

public record UpdateSkillCommand : IRequest<SkillDto>
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;
}

public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, SkillDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateSkillCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SkillDto> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Skills
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Skill), request.Id);
        }

        entity.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<SkillDto>(entity);

        return result;
    }
}
