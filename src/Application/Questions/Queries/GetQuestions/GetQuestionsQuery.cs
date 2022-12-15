using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Questions.Queries.GetQuestions;

public record GetQuestionsQuery : IRequest<List<QuestionDto>>;

public class GetQuestionsQueryHandler : IRequestHandler<GetQuestionsQuery, List<QuestionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetQuestionsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<QuestionDto>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Questions
            .OrderBy(x => x.Id)
            .OrderBy(x => x.QuizId)
            .ProjectTo<QuestionDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
