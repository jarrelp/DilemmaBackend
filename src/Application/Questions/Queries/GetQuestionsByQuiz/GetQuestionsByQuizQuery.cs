using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Questions.Queries.GetQuestionsByQuiz;

public record GetQuestionsByQuizQuery(int Id) : IRequest<List<QuestionDto>>;

public class GetQuestionsByQuizQueryHandler : IRequestHandler<GetQuestionsByQuizQuery, List<QuestionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetQuestionsByQuizQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<QuestionDto>> Handle(GetQuestionsByQuizQuery request, CancellationToken cancellationToken)
    {
        return await _context.Questions
            .Where(x => x.QuizId == request.Id)
            .ProjectTo<QuestionDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
