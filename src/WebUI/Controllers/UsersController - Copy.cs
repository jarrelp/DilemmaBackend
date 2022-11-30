using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Users.Queries.GetUsersWithPagination;
using CleanArchitecture.Application.Users.Commands.CreateUser;
using CleanArchitecture.Application.Users.Commands.DeleteUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers;

//[Authorize]
public class UsersController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ApplicationUserDto>>> GetUsersWithPagination([FromQuery] GetUsersWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateUserCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        await Mediator.Send(new DeleteUserCommand(id));

        return NoContent();
    }
}
