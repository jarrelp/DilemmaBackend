using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;

namespace CleanArchitecture.Application.Results.Queries.GetResultsWithPagination;

public record GetResultsWithPaginationQuery : IRequest<PaginatedList<ResultDto>>
{
    public int? ResultId { get; init; }
    public string? ApplicationUserId { get; init; }
    public int? QuizId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetResultsWithPaginationQueryHandler : IRequestHandler<GetResultsWithPaginationQuery, PaginatedList<ResultDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetResultsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ResultDto>> Handle(GetResultsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Results
            /*.Where(x => x.Id == request.ResultId || x.ApplicationUserId == request.ApplicationUserId || x.QuizId == request.QuizId)*/
            .OrderBy(x => x.Id)
            .ProjectTo<ResultDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
