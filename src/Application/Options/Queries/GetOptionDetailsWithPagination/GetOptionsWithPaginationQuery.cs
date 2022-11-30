using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;

namespace CleanArchitecture.Application.Options.Queries.GetOptionDetailsWithPagination;

public record GetOptionDetailsWithPaginationQuery : IRequest<PaginatedList<OptionDetailDto>>
{
    public int? OptionId { get; init; }
    public int? QuestionId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetOptionDetailsWithPaginationQueryHandler : IRequestHandler<GetOptionDetailsWithPaginationQuery, PaginatedList<OptionDetailDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetOptionDetailsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<OptionDetailDto>> Handle(GetOptionDetailsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var ret = new PaginatedList<OptionDetailDto>();
        if (request.OptionId != null && request.QuestionId != null)
        {
            ret = await _context.Options
            .Where(x => x.Id == request.OptionId && x.QuestionId == request.QuestionId)
            .OrderBy(x => x.Description)
            .ProjectTo<OptionDetailDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else if (request.OptionId != null && request.QuestionId == null)
        {
            ret = await _context.Options
            .Where(x => x.Id == request.OptionId)
            .OrderBy(x => x.Description)
            .ProjectTo<OptionDetailDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else if (request.OptionId == null && request.QuestionId != null)
        {
            ret = await _context.Options
            .Where(x => x.QuestionId == request.QuestionId)
            .OrderBy(x => x.Description)
            .ProjectTo<OptionDetailDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            ret = await _context.Options
            .OrderBy(x => x.Description)
            .ProjectTo<OptionDetailDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        }

        return ret;
    }
}
