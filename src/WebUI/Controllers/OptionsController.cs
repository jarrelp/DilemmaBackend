using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Options.Commands.CreateOption;
using CleanArchitecture.Application.Options.Commands.DeleteOption;
using CleanArchitecture.Application.Options.Commands.UpdateOption;
using CleanArchitecture.Application.Options.Queries.GetOptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers;

//[Authorize]
public class OptionsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<OptionDto>>> GetOptions([FromQuery] GetOptionsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateOptionCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateOptionCommand command)
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
        await Mediator.Send(new DeleteOptionCommand(id));

        return NoContent();
    }
}
