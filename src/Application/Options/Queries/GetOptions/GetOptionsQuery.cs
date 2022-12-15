using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Options.Queries.GetOptions;

public record GetOptionsQuery : IRequest<List<OptionDto>>;

public class GetOptionsQueryHandler : IRequestHandler<GetOptionsQuery, List<OptionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetOptionsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OptionDto>> Handle(GetOptionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Options
            .OrderBy(x => x.Id)
            .OrderBy(x => x.QuestionId)
            .OrderBy(x => x.Question.QuizId)
            .ProjectTo<OptionDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
