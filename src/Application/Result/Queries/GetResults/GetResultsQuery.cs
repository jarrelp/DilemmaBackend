using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Results.Queries.GetResults;

public record GetResultsQuery : IRequest<List<ResultDto>>;

public class GetResultsWithPaginationQueryHandler : IRequestHandler<GetResultsQuery, List<ResultDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetResultsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ResultDto>> Handle(GetResultsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Results
            .ProjectTo<ResultDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
