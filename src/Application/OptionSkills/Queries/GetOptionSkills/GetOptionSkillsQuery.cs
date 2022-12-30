using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.OptionSkills.Queries.GetOptionSkills;

public record GetOptionSkillsQuery : IRequest<List<OptionSkillDto>>;

public class GetOptionSkillsQueryHandler : IRequestHandler<GetOptionSkillsQuery, List<OptionSkillDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetOptionSkillsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OptionSkillDto>> Handle(GetOptionSkillsQuery request, CancellationToken cancellationToken)
    {
        return await _context.OptionSkills
            .ProjectTo<OptionSkillDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
