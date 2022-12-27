using CleanArchitecture.Application.Quizzes.Commands.CreateQuiz;
using CleanArchitecture.Application.Quizzes.Commands.DeleteQuiz;
using CleanArchitecture.Application.Quizzes.Commands.UpdateQuiz;
using CleanArchitecture.Application.Quizzes.Queries.GetQuizzes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.API.Controllers;

[Authorize]
public class QuizzesController : ApiControllerBase
{
    [HttpGet]
    [ResponseCache(CacheProfileName = "30SecondsCaching")]
    public async Task<ActionResult<List<QuizDto>>> GetQuizzes([FromQuery] GetQuizzesQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<QuizDto>> Create(CreateQuizCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<QuizDto>> Update(int id, UpdateQuizCommand command)
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
        await Mediator.Send(new DeleteQuizCommand(id));

        return NoContent();
    }
}
