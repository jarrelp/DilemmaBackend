using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;

namespace CleanArchitecture.Application.Questions.Queries.GetQuestionsWithPagination;

public record GetQuestionsWithPaginationQuery : IRequest<PaginatedList<QuestionDto>>
{
    public int? QuestionId { get; init; }
    public int? QuizId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetQuestionsWithPaginationQueryHandler : IRequestHandler<GetQuestionsWithPaginationQuery, PaginatedList<QuestionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetQuestionsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<QuestionDto>> Handle(GetQuestionsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var ret = new PaginatedList<QuestionDto>();
        if (request.QuestionId != null && request.QuizId != null)
        {
            ret = await _context.Questions
            .Where(x => x.Id == request.QuestionId && x.QuizId == request.QuizId)
            .OrderBy(x => x.Description)
            .ProjectTo<QuestionDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else if (request.QuestionId != null && request.QuizId == null)
        {
            ret = await _context.Questions
            .Where(x => x.Id == request.QuestionId)
            .OrderBy(x => x.Description)
            .ProjectTo<QuestionDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else if (request.QuestionId == null && request.QuizId != null)
        {
            ret = await _context.Questions
            .Where(x => x.QuizId == request.QuizId)
            .OrderBy(x => x.Description)
            .ProjectTo<QuestionDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            ret = await _context.Questions
            .OrderBy(x => x.Description)
            .ProjectTo<QuestionDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        }

        return ret;
    }
}
