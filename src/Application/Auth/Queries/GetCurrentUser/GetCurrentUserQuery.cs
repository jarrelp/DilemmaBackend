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
        if (_currentUserService.UserName == null)
        {
            throw new UnauthorizedAccessException();
        }

        var user = await _identityService.GetUserByUserNameAsync(_currentUserService.UserName);
        var retUser = _mapper.Map<ApplicationUserDto>(user);
        return retUser;
    }
}
