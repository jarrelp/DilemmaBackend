using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Users.Queries.GetUsersByDepartment;

public record GetUsersByDepartmentQuery(int Id) : IRequest<List<ApplicationUserDto>>;

public class GetUsersByDepartmentQueryHandler : IRequestHandler<GetUsersByDepartmentQuery, List<ApplicationUserDto>>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public GetUsersByDepartmentQueryHandler(
        IIdentityService identityService,
        IMapper mapper
        )
    {
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<List<ApplicationUserDto>> Handle(GetUsersByDepartmentQuery request, CancellationToken cancellationToken)
    {
        var ret = await _identityService.GetAllUsersByDepartmentAsync(request.Id);
        var ret2 = ret.ToList().AsQueryable();
        return ret2
            .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider).ToList();
    }
}
