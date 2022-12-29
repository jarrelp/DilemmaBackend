using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Users.Queries.GetUsers;
using CleanArchitecture.Application.Users.Commands.CreateUser;
using CleanArchitecture.Application.Users.Commands.DeleteUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Users.Commands.UpdateUser;
using CleanArchitecture.Application.Users.Queries.GetUsersByDepartment;

namespace CleanArchitecture.API.Controllers;

/*[Authorize]*/
public class UsersController : ApiControllerBase
{
    [HttpGet]
    /*[ResponseCache(CacheProfileName = "30SecondsCaching")]*/
    public async Task<ActionResult<List<ApplicationUserDto>>> GetUsers([FromQuery] GetUsersQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("department/{id}")]
    /*[ResponseCache(CacheProfileName = "30SecondsCaching")]*/
    public async Task<ActionResult<List<ApplicationUserDto>>> GetUsersByDepartment(int id)
    {
        return await Mediator.Send(new GetUsersByDepartmentQuery(id));
    }

    [HttpPost]
    public async Task<ActionResult<ApplicationUserDto>> Create(CreateUserCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApplicationUserDto>> Update(string id, UpdateUserCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(id);
        }

        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> Delete(string id)
    {
        return await Mediator.Send(new DeleteUserCommand(id));
    }
}
