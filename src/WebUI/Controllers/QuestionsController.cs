using CleanArchitecture.Application.Questions.Commands.CreateQuestion;
using CleanArchitecture.Application.Questions.Commands.DeleteQuestion;
using CleanArchitecture.Application.Questions.Commands.UpdateQuestion;
using CleanArchitecture.Application.Questions.Queries.GetQuestions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.SkillLevels.Queries.GetSkillLevels;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Questions.Queries.GetQuestionsByQuiz;
using CleanArchitecture.Application.Questions.Queries.GetQuestionsByActiveQuiz;

namespace CleanArchitecture.API.Controllers;

/*[Authorize]*/
public class QuestionsController : ApiControllerBase
{
    [HttpGet]
    /*[ResponseCache(CacheProfileName = "30SecondsCaching")]*/
    public async Task<ActionResult<List<QuestionDto>>> GetQuestions([FromQuery] GetQuestionsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("ByQuiz/{id}")]
    /*[ResponseCache(CacheProfileName = "30SecondsCaching")]*/
    public async Task<ActionResult<List<QuestionDto>>> GetQuestionsByQuiz(int id)
    {
        return await Mediator.Send(new GetQuestionsByQuizQuery(id));
    }

    [HttpGet("ByActiveQuiz")]
    /*[ResponseCache(CacheProfileName = "30SecondsCaching")]*/
    public async Task<ActionResult<List<ActiveQuestionDto>>> GetQuestionsByActiveQuiz([FromQuery] GetQuestionsByActiveQuizQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("skilllevels")]
    /*[ResponseCache(CacheProfileName = "30SecondsCaching")]*/
    public async Task<ActionResult<List<SkillLevelDto>>> GetSkillLevels()
    {
        return await Mediator.Send(new GetSkillLevelsQuery());
    }

    [HttpPost]
    public async Task<ActionResult<QuestionDto>> Create(CreateQuestionCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<QuestionDto>> Update(int id, UpdateQuestionCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        return await Mediator.Send(new DeleteQuestionCommand(id));
    }
}
