using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Quizzes.Queries.GetQuizzes;

public record GetQuizzesQuery : IRequest<List<QuizDto>>;

public class GetQuizzesQueryHandler : IRequestHandler<GetQuizzesQuery, List<QuizDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetQuizzesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<QuizDto>> Handle(GetQuizzesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Quizzes
            .OrderBy(x => x.Id)
            .ProjectTo<QuizDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
