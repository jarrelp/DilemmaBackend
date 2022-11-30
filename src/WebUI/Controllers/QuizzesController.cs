using CleanArchitecture.Application.Quizzes.Commands.CreateQuiz;
using CleanArchitecture.Application.Quizzes.Commands.DeleteQuiz;
using CleanArchitecture.Application.Quizzes.Commands.UpdateQuiz;
using CleanArchitecture.Application.Quizzes.Commands.PurgeQuizzes;
using CleanArchitecture.Application.Quizzes.Queries.GetQuizzesWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.API.Controllers;

//[Authorize]
public class QuizzesController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<QuizDto>>> GetQuizzesWithPagination([FromQuery] GetQuizzesWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateQuizCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateQuizCommand command)
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

    [HttpDelete]
    public async Task<ActionResult> Purge()
    {
        await Mediator.Send(new PurgeQuizzesCommand());

        return NoContent();
    }
}
