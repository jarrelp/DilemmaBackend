using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.OptionSkills.Commands.CreateOptionSkill;
using CleanArchitecture.Application.OptionSkills.Commands.DeleteOptionSkill;
using CleanArchitecture.Application.OptionSkills.Commands.UpdateOptionSkill;
using CleanArchitecture.Application.OptionSkills.Queries.GetOptionSkills;
using CleanArchitecture.Application.OptionSkills.Queries.GetOptionSkillsByOption;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers;

/*[Authorize]*/
public class OptionSkillsController : ApiControllerBase
{
    [HttpGet]
    /*[ResponseCache(CacheProfileName = "30SecondsCaching")]*/
    public async Task<ActionResult<List<OptionSkillDto>>> GetOptionSkills([FromQuery] GetOptionSkillsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("ByOption/{id}")]
    /*[ResponseCache(CacheProfileName = "30SecondsCaching")]*/
    public async Task<ActionResult<List<OptionSkillDto>>> GetOptionSkillsByOption(int id)
    {
        return await Mediator.Send(new GetOptionSkillsByOptionQuery(id));
    }

    [HttpPost]
    public async Task<ActionResult<OptionSkillDto>> Create(CreateOptionSkillCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<OptionSkillDto>> Update(string id, UpdateOptionSkillCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> Delete(string id)
    {
        return await Mediator.Send(new DeleteOptionSkillCommand(id));
    }
}
