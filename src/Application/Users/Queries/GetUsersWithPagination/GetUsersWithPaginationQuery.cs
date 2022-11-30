using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Users.Queries.GetUsersWithPagination;

public record GetUsersWithPaginationQuery : IRequest<PaginatedList<ApplicationUserDto>>
{
    public string? UserId { get; init; }
    public string? UserName { get; init; }
    public int? DepartmentId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetUsersWithPaginationQueryHandler : IRequestHandler<GetUsersWithPaginationQuery, PaginatedList<ApplicationUserDto>>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public GetUsersWithPaginationQueryHandler(
        IIdentityService identityService,
        IMapper mapper
        )
    {
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ApplicationUserDto>> Handle(GetUsersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var ret = new PaginatedList<ApplicationUserDto>();
        var results = _identityService.GetAllUsersAsync().Result.ToList();
        ret.Items.AddRange((IEnumerable<ApplicationUserDto>)results);

        /*if (request.UserId == null && request.UserName == null && request.DepartmentId == null)
        {
            ret = await _identityService.GetAllUsersAsync().ToList()
            .Where(x => x.Id == request.UserId && x.UserName == request.UserName && x.DepartmentId == request.DepartmentId)
            .OrderBy(x => x.UserName)
            .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else if (request.UserId != null && request.UserName == null && request.DepartmentId == null)
        {
            ret = await _identityService.GetAllUsersAsync().Result.AsQueryable()
            .Where(x => x.Id == request.UserId)
            .OrderBy(x => x.UserName)
            .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else if (request.UserId == null && request.UserName != null && request.DepartmentId == null)
        {
            ret = await _identityService.GetAllUsersAsync().Result.AsQueryable()
            .Where(x => x.UserName == request.UserName)
            .OrderBy(x => x.UserName)
            .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else if (request.UserId == null && request.UserName == null && request.DepartmentId != null)
        {
            ret = await _identityService.GetAllUsersAsync().Result.AsQueryable()
            .Where(x => x.DepartmentId == request.DepartmentId)
            .OrderBy(x => x.UserName)
            .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else if (request.UserId != null && request.UserName != null && request.DepartmentId == null)
        {
            ret = await _identityService.GetAllUsersAsync().Result.AsQueryable()
            .Where(x => x.Id == request.UserId && x.UserName == request.UserName)
            .OrderBy(x => x.UserName)
            .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else if (request.UserId != null && request.UserName == null && request.DepartmentId != null)
        {
            ret = await _identityService.GetAllUsersAsync().Result.AsQueryable()
            .Where(x => x.Id == request.UserId && x.DepartmentId == request.DepartmentId)
            .OrderBy(x => x.UserName)
            .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else if (request.UserId == null && request.UserName != null && request.DepartmentId != null)
        {
            ret = await _identityService.GetAllUsersAsync().Result.AsQueryable()
            .Where(x => x.UserName == request.UserName && x.DepartmentId == request.DepartmentId)
            .OrderBy(x => x.UserName)
            .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            ret = await _identityService.GetAllUsersAsync().Result.AsQueryable()
            .OrderBy(x => x.UserName)
            .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        }*/

        return await Task.FromResult(ret);
    }
}
