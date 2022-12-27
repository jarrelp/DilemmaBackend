using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Questions.Queries.GetQuestionsByActiveQuiz;

public record GetQuestionsByActiveQuizQuery : IRequest<List<ActiveQuestionDto>>;

public class GetQuestionsByActiveQuizQueryHandler : IRequestHandler<GetQuestionsByActiveQuizQuery, List<ActiveQuestionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetQuestionsByActiveQuizQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ActiveQuestionDto>> Handle(GetQuestionsByActiveQuizQuery request, CancellationToken cancellationToken)
    {
        var quiz = _context.Quizzes.Where(x => x.Active == true).First();

        var questions = _context.Questions.Where(x => x.QuizId == quiz.Id);

        return questions.AsQueryable().ProjectTo<ActiveQuestionDto>(_mapper.ConfigurationProvider).ToList();
    }
}
