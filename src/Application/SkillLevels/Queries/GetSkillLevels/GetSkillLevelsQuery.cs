using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.SkillLevels.Queries.GetSkillLevels;

//[Authorize]
public record GetSkillLevelsQuery : IRequest<List<SkillLevelDto>>;

public class GetSkillLevelsQueryHandler : IRequestHandler<GetSkillLevelsQuery, List<SkillLevelDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSkillLevelsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<SkillLevelDto>> Handle(GetSkillLevelsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Enum.GetValues(typeof(SkillLevel))
                .Cast<SkillLevel>()
                .Select(p => new SkillLevelDto { Value = (int)p, Name = p.ToString() })
                .ToList());
    }
}
