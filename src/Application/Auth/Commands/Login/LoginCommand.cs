using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Auth.Commands.Login;

public record LoginCommand : IRequest<TokenDto>
{
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenDto>
{
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly IMapper _mapper;

    public LoginCommandHandler(IUserAuthenticationService userAuthenticationService, IMapper mapper)
    {
        _userAuthenticationService = userAuthenticationService;
        _mapper = mapper;
    }

    public async Task<TokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userAuthenticationService.GetUser(request.UserName);
        var retUser = new ApplicationUserDto
        {
            UserName = user.UserName,
            DepartmentId= user.DepartmentId,
            Id= user.Id
        };
        /*var ret = await _identityService.GetAllUsersAsync();
        var ret2 = ret.ToList().AsQueryable();
        return ret2
            .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider).ToList();*/

        return !await _userAuthenticationService.ValidateUserAsync(request.UserName, request.Password)
            ? throw new NotFoundException(request.UserName)
            : new TokenDto { Token = await _userAuthenticationService.CreateTokenAsync(), User = retUser };
    }
}

public class TokenDto
{
    public string Token { get; set; } = null!;
    public ApplicationUserDto User { get; set; } = null!;
}
