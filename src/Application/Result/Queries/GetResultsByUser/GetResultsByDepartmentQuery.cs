using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Results.Queries.GetResultsByUser;

public record GetResultsByUserQuery : IRequest<List<ResultDto>>
{
    public string UserId { get; init; } = null!;
    public int QuizId { get; init; }
}

public class GetResultsByUserWithPaginationQueryHandler : IRequestHandler<GetResultsByUserQuery, List<ResultDto>>
{
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;

    public GetResultsByUserWithPaginationQueryHandler(IMapper mapper, IIdentityService identityService, IApplicationDbContext context)
    {
        _mapper = mapper;
        _identityService = identityService;
        _context = context;
    }

    public async Task<List<ResultDto>> Handle(GetResultsByUserQuery request, CancellationToken cancellationToken)
    {
        var users = await _identityService.GetUserAsync(request.UserId);

        var results = new List<Domain.Entities.Result>();

        var resultList = _context.Results.Where(x => x.ApplicationUserId == request.UserId && x.QuizId == request.QuizId).Include(x => x.Answers).ToList();
        results.AddRange(resultList);

        return results.AsQueryable().ProjectTo<ResultDto>(_mapper.ConfigurationProvider).ToList();
    }
}
