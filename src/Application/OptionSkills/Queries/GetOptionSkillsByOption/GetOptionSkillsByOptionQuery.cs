using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.OptionSkills.Queries.GetOptionSkillsByOption;

public record GetOptionSkillsByOptionQuery(int Id) : IRequest<List<OptionSkillDto>>;

public class GetOptionSkillsByOptionQueryHandler : IRequestHandler<GetOptionSkillsByOptionQuery, List<OptionSkillDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetOptionSkillsByOptionQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OptionSkillDto>> Handle(GetOptionSkillsByOptionQuery request, CancellationToken cancellationToken)
    {
        /*string[] ids = request.Id.Split('-');

        var entity = await _context.OptionSkills
            .Where(x => x.OptionId == Int32.Parse(ids[0]) && x.SkillId == Int32.Parse(ids[1]))
            .ProjectTo<OptionSkillDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return entity;*/

        return await _context.OptionSkills
            .Where(x => x.OptionId == request.Id)
            .ProjectTo<OptionSkillDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
