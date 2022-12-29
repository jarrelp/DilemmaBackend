using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Quiz;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Quizzes.Commands.DeleteQuiz;

public record DeleteQuizCommand(int Id) : IRequest<int>;

public class DeleteQuizCommandHandler : IRequestHandler<DeleteQuizCommand, int>
{
    private readonly IApplicationDbContext _context;

    public DeleteQuizCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Quizzes
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Quiz), request.Id);
        }

        _context.Quizzes.Remove(entity);

        var questions = _context.Questions.Where(x => x.QuizId == entity.Id).ToList();

        _context.Questions.RemoveRange(questions);

        entity.AddDomainEvent(new QuizDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
