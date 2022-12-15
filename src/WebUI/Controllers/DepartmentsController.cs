using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Departments.Queries.GetDepartments;
using CleanArchitecture.Application.Departments.Commands.CreateDepartment;
using CleanArchitecture.Application.Departments.Commands.DeleteDepartment;
using CleanArchitecture.Application.Departments.Commands.DeleteDepartments;
using CleanArchitecture.Application.Departments.Commands.UpdateDepartment;
using CleanArchitecture.Application.Departments.Commands.PurgeDepartments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers;

//[Authorize]
public class DepartmentsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<DepartmentDto>>> GetDepartments([FromQuery] GetDepartmentQuery query)
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

    [HttpDelete("multiple")]
    public async Task<ActionResult<int[]>> Delete(DeleteDepartmentsCommand command)
    {
        if (command.Ids.Length == 0)
        {
            return BadRequest();
        }
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> Purge()
    {
        await Mediator.Send(new PurgeDepartmentsCommand());

        return NoContent();
    }
}
