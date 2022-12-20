using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Auth.Commands.Login;
using CleanArchitecture.Application.Auth.Commands.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Application.Departments.Queries.GetDepartments;
using CleanArchitecture.Application.Questions.Queries.GetQuestions;
using MediatR;
using CleanArchitecture.Application.Auth.Queries.GetToken;
using CleanArchitecture.Application.Auth.Commands;

namespace CleanArchitecture.API.Controllers;

public class AuthController : ApiControllerBase
{
    [HttpGet("me")]
    [ResponseCache(CacheProfileName = "30SecondsCaching")]
    public async Task<ActionResult<ApplicationUserDto>> GetCurrentUser([FromQuery] GetCurrentUserQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthDto>> Login(LoginCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthDto>> Register(RegisterCommand command)
    {
        return await Mediator.Send(command);
    }
}
