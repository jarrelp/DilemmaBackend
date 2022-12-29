using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Options.Commands.CreateOption;
using CleanArchitecture.Application.Options.Commands.DeleteOption;
using CleanArchitecture.Application.Options.Commands.UpdateOption;
using CleanArchitecture.Application.Options.Queries.GetOptions;
using CleanArchitecture.Application.Options.Queries.GetOptionsByQuestion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers;

/*[Authorize]*/
public class OptionsController : ApiControllerBase
{
    [HttpGet]
    /*[ResponseCache(CacheProfileName = "30SecondsCaching")]*/
    public async Task<ActionResult<List<OptionDto>>> GetOptions([FromQuery] GetOptionsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("ByQuestion/{id}")]
    /*[ResponseCache(CacheProfileName = "30SecondsCaching")]*/
    public async Task<ActionResult<List<OptionDto>>> GetOptionsByDepartment(int id)
    {
        return await Mediator.Send(new GetOptionsByQuestionQuery(id));
    }

    [HttpGet("ByDepartment")]
    /*[ResponseCache(CacheProfileName = "30SecondsCaching")]*/
    public async Task<ActionResult<List<OptionDto>>> GetOptionsByDepartment([FromQuery] GetOptionsByQuestionQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<OptionDto>> Create(CreateOptionCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<OptionDto>> Update(int id, UpdateOptionCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        return await Mediator.Send(new DeleteOptionCommand(id));
    }
}
