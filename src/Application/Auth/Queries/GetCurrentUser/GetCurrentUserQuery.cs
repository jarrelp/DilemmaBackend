using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Auth.Queries.GetToken;

public record GetCurrentUserQuery : IRequest<ApplicationUserDto>;

public class GetCurrentUserHandler : IRequestHandler<GetCurrentUserQuery, ApplicationUserDto>
{
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public GetCurrentUserHandler(
        IMapper mapper,
        ICurrentUserService currentUserService,
        IIdentityService identityService)
    {
        _mapper = mapper;
        _currentUserService = currentUserService;
        _identityService = identityService;
    }

    public async Task<ApplicationUserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
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

        /*var currentUserId = _currentUserService.UserId;
        if (currentUserId == null)
        {
            throw new NotFoundException(nameof(ApplicationUser), currentUserId);
        }*/
        if (_currentUserService.UserName == null)
        {
            throw new UnauthorizedAccessException();
        }

        var user = await _identityService.GetUserByUserNameAsync(_currentUserService.UserName);
        var retUser = new ApplicationUserDto
        {
            UserName = user.UserName,
            DepartmentId = user.DepartmentId,
            Id = user.Id
        };
        return retUser;

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
