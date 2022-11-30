using CleanArchitecture.Application.Questions.Commands.CreateQuestion;
using CleanArchitecture.Application.Questions.Commands.DeleteQuestion;
using CleanArchitecture.Application.Questions.Commands.UpdateQuestion;
using CleanArchitecture.Application.Questions.Commands.PurgeQuestions;
using CleanArchitecture.Application.Questions.Queries.GetQuestionsWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.SkillLevels.Queries.GetSkillLevels;
using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.API.Controllers;

//[Authorize]
public class QuestionsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<QuestionDto>>> GetQuestionsWithPagination([FromQuery] GetQuestionsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("skilllevels")]
    public async Task<ActionResult<List<SkillLevelDto>>> GetSkillLevels()
    {
        return await Mediator.Send(new GetSkillLevelsQuery());
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateQuestionCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateQuestionCommand command)
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
        await Mediator.Send(new DeleteQuestionCommand(id));

        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> Purge()
    {
        await Mediator.Send(new PurgeQuestionsCommand());

        return NoContent();
    }
}
