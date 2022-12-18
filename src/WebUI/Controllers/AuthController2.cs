/*using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController2 : ControllerBase
{
    private readonly IUserAuthenticationService _authenticationService;

    public AuthController2(IUserAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    *//*[HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] string userName, string password,  int departmentId)
    {

        var userResult = await _authenticationService.RegisterUserAsync(userRegistration);
        return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : StatusCode(201);
    }*//*

    [HttpPost("login")]
    public async Task<TokenDto> Authenticate([FromBody] LoginDto loginDto)
    {
        return !await _authenticationService.ValidateUserAsync(loginDto.UserName, loginDto.Password)
            ? throw new Exception()
            : new TokenDto { Token = await _authenticationService.CreateTokenAsync() };
    }
}

public class TokenDto
{
    public string Token { get; set; }
}

public class LoginDto
{
    public string? UserName { get; init; }
    public string? Password { get; init; }
}*/