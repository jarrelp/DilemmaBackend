using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
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

    public LoginCommandHandler(IUserAuthenticationService userAuthenticationService)
    {
        _userAuthenticationService = userAuthenticationService;
    }

    public async Task<TokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return !await _userAuthenticationService.ValidateUserAsync(request.UserName, request.Password)
            ? throw new NotFoundException(request.UserName)
            : new TokenDto { Value = await _userAuthenticationService.CreateTokenAsync() };
    }
}

public class TokenDto
{
    public string Value { get; set; } = null!;
}
