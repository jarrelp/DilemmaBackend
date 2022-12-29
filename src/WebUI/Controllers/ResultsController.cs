using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Results.Commands.CreateResult;
using CleanArchitecture.Application.Results.Commands.DeleteResult;
using CleanArchitecture.Application.Results.Queries.GetResults;
using CleanArchitecture.Application.Results.Queries.GetResultsByDepartment;
using CleanArchitecture.Application.Results.Queries.GetResultsByUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers;

/*[Authorize]*/
public class ResultsController : ApiControllerBase
{
    [HttpGet]
    /*[ResponseCache(CacheProfileName = "30SecondsCaching")]*/
    public async Task<ActionResult<List<ResultDto>>> GetResults([FromQuery] GetResultsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("department")]
    /*[ResponseCache(CacheProfileName = "30SecondsCaching")]*/
    public async Task<ActionResult<List<ResultDto>>> GetResultsByDepartment([FromQuery] GetResultsByDepartmentQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("user")]
    /*[ResponseCache(CacheProfileName = "30SecondsCaching")]*/
    public async Task<ActionResult<List<ResultDto>>> GetResultsByUser([FromQuery] GetResultsByUserQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<ResultDto>> Create(CreateResultCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteResultCommand(id));

        return NoContent();
    }
}
