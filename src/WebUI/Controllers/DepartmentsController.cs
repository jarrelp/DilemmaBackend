using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Departments.Queries.GetDepartmentsWithPagination;
using CleanArchitecture.Application.Departments.Commands.CreateDepartment;
using CleanArchitecture.Application.Departments.Commands.DeleteDepartment;
using CleanArchitecture.Application.Departments.Commands.UpdateDepartment;
using CleanArchitecture.Application.Departments.Commands.PurgeDepartments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers;

//[Authorize]
public class DepartmentsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<DepartmentDto>>> GetDepartmentsWithPagination([FromQuery] GetDepartmentsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateDepartmentCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateDepartmentCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteDepartmentCommand(id));

        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> Purge()
    {
        await Mediator.Send(new PurgeDepartmentsCommand());

        return NoContent();
    }
}
