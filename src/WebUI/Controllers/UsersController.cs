using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Users.Queries.GetUsers;
using CleanArchitecture.Application.Users.Commands.CreateUser;
using CleanArchitecture.Application.Users.Commands.DeleteUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Application.Users.Commands.UpdateUser;
using CleanArchitecture.Application.Users.Queries.GetUsersByDepartment;

namespace CleanArchitecture.API.Controllers;

//[Authorize]
public class UsersController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ApplicationUserDto>>> GetUsers([FromQuery] GetUsersQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("department/{id}")]
    public async Task<ActionResult<List<ApplicationUserDto>>> GetUsersByDepartment(int id)
    {
        return await Mediator.Send(new GetUsersByDepartmentQuery(id));
    }

    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateUserCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<string>> Update(string id, UpdateUserCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(id);
        }

        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        await Mediator.Send(new DeleteUserCommand(id));

        return NoContent();
    }
}
