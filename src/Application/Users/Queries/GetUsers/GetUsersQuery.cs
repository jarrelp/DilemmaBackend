using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Users.Queries.GetUsers;

public record GetUsersQuery : IRequest<List<ApplicationUserDto>>;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<ApplicationUserDto>>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetUsersQueryHandler(
        IIdentityService identityService,
        IMapper mapper,
        IApplicationDbContext context)
    {
        _identityService = identityService;
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<ApplicationUserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        /*var ret = new List<ApplicationUserDto>();
        var result = _identityService.GetAllUsersAsync().Result;
        if (result.Any())
        {
            ret.AddRange((IEnumerable<ApplicationUserDto>)result);
        }
        return await ret.AsQueryable()
            *//*.OrderBy(x => x.UserName)*//*
            .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
            .ToListAsync();*/

        var ret = await _identityService.GetAllUsersAsync();
        var ret2 = ret.ToList().AsQueryable();
        /*foreach(var item in ret2)
        {
            
        }*/
        return ret2
            .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider).ToList();

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
    }
}
