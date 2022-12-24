using CleanArchitecture.Application.Questions.Commands.CreateQuestion;
using CleanArchitecture.Application.Questions.Commands.DeleteQuestion;
using CleanArchitecture.Application.Questions.Commands.UpdateQuestion;
using CleanArchitecture.Application.Questions.Commands.PurgeQuestions;
using CleanArchitecture.Application.Questions.Queries.GetQuestions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.SkillLevels.Queries.GetSkillLevels;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Questions.Queries.GetQuestionsByQuiz;

namespace CleanArchitecture.API.Controllers;

[Authorize]
public class QuestionsController : ApiControllerBase
{
    [HttpGet]
    [ResponseCache(CacheProfileName = "30SecondsCaching")]
    public async Task<ActionResult<List<QuestionDto>>> GetQuestions([FromQuery] GetQuestionsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("ByQuiz")]
    [ResponseCache(CacheProfileName = "30SecondsCaching")]
    public async Task<ActionResult<List<QuestionDto>>> GetQuestionsByQuiz([FromQuery] GetQuestionsByQuizQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("skilllevels")]
    [ResponseCache(CacheProfileName = "30SecondsCaching")]
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
