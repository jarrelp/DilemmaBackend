using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Results.Queries.GetResultsByDepartment;

public record GetResultsByDepartmentQuery : IRequest<List<ResultDto>>
{
    public int DepartmentId { get; init; }
    public int QuizId { get; init; }
}

public class GetResultsByDepartmentWithPaginationQueryHandler : IRequestHandler<GetResultsByDepartmentQuery, List<ResultDto>>
{
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetResultsByDepartmentWithPaginationQueryHandler(IMapper mapper, IIdentityService identityService)
    {
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<List<ResultDto>> Handle(GetResultsByDepartmentQuery request, CancellationToken cancellationToken)
    {
        var users = await _identityService.GetAllUsersByDepartmentAsync(request.DepartmentId);
        var userList = users.ToList();

        var results = new List<Domain.Entities.Result>();
        if(userList.Any())
        {
            foreach (var item1 in userList)
            {
                if (item1.Results.Any())
                {
                    foreach (var item2 in item1.Results)
                    {
                        if (item2.QuizId == request.QuizId)
                        {
                            results.Add(item2);
                        }
                    }
                }
            }
        }

        return results.AsQueryable().ProjectTo<ResultDto>(_mapper.ConfigurationProvider).ToList();
    }
}
