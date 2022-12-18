using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Auth.Commands.Login;
using CleanArchitecture.Application.Auth.Commands.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.API.Controllers;

public class AuthController : ApiControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<TokenDto>> Login(LoginCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("register")]
    public async Task<ActionResult<string>> Register(RegisterCommand command)
    {
        return await Mediator.Send(command);
    }
}
