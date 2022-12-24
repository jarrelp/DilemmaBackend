using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Options.Queries.GetOptionSkillsByOption;

public record GetOptionSkillsByOptionQuery : IRequest<List<OptionDto>>;

public class GetOptionSkillsByOptionQueryHandler : IRequestHandler<GetOptionSkillsByOptionQuery, List<OptionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetOptionSkillsByOptionQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OptionDto>> Handle(GetOptionSkillsByOptionQuery request, CancellationToken cancellationToken)
    {
        return await _context.Options
            .OrderBy(x => x.Id)
            .OrderBy(x => x.QuestionId)
            .OrderBy(x => x.Question.QuizId)
            .ProjectTo<OptionDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
