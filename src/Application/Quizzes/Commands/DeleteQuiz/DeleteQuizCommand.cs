using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Quiz;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Quizzes.Commands.DeleteQuiz;

public record DeleteQuizCommand(int Id) : IRequest;

public class DeleteQuizCommandHandler : IRequestHandler<DeleteQuizCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteQuizCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Quizzes
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Quiz), request.Id);
        }

        _context.Quizzes.Remove(entity);

        entity.AddDomainEvent(new QuizDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
