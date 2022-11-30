using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;

namespace CleanArchitecture.Application.Skills.Queries.GetSkillsWithPagination;

public record GetSkillsWithPaginationQuery : IRequest<PaginatedList<SkillDto>>
{
    public int? SkillId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetSkillsWithPaginationQueryHandler : IRequestHandler<GetSkillsWithPaginationQuery, PaginatedList<SkillDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSkillsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<SkillDto>> Handle(GetSkillsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var ret = new PaginatedList<SkillDto>();
        ret = request.SkillId == null
            ? await _context.Skills
            .OrderBy(x => x.Name)
            .ProjectTo<SkillDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize)
            : await _context.Skills
            .Where(x => x.Id == request.SkillId)
            .OrderBy(x => x.Name)
            .ProjectTo<SkillDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        return ret;
    }
}
