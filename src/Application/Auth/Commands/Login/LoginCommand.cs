using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Auth.Commands.Login;

public record LoginCommand : IRequest<AuthDto>
{
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthDto>
{
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly IMapper _mapper;

    public LoginCommandHandler(IUserAuthenticationService userAuthenticationService, IMapper mapper)
    {
        _userAuthenticationService = userAuthenticationService;
        _mapper = mapper;
    }

    public async Task<AuthDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userAuthenticationService.GetUser(request.UserName);
        var retUser = _mapper.Map<ApplicationUserDto>(user);

        return !await _userAuthenticationService.ValidateUserAsync(request.UserName, request.Password)
            ? throw new NotFoundException(request.UserName)
            : new AuthDto { Token = await _userAuthenticationService.CreateTokenAsync(), User = retUser };
    }
}
