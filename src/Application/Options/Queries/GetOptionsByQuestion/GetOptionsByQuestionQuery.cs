using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Options.Queries.GetOptionsByQuestion;

public record GetOptionsByQuestionQuery(int Id) : IRequest<List<OptionDto>>;

public class GetOptionsByQuestionQueryHandler : IRequestHandler<GetOptionsByQuestionQuery, List<OptionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetOptionsByQuestionQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OptionDto>> Handle(GetOptionsByQuestionQuery request, CancellationToken cancellationToken)
    {
        return await _context.Options
            .Where(x => x.QuestionId == request.Id)
            .ProjectTo<OptionDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
