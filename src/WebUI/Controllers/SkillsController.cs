using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Skills.Queries.GetSkills;
using CleanArchitecture.Application.Skills.Commands.CreateSkill;
using CleanArchitecture.Application.Skills.Commands.DeleteSkill;
using CleanArchitecture.Application.Skills.Commands.UpdateSkill;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers;

[Authorize]
public class SkillsController : ApiControllerBase
{
    [HttpGet]
    [ResponseCache(CacheProfileName = "30SecondsCaching")]
    public async Task<ActionResult<List<SkillDto>>> GetSkills([FromQuery] GetSkillsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<SkillDto>> Create(CreateSkillCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SkillDto>> Update(int id, UpdateSkillCommand command)
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
        await Mediator.Send(new DeleteSkillCommand(id));

        return NoContent();
    }
}
