using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Question;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Questions.Commands.DeleteQuestion;

public record DeleteQuestionCommand(int Id) : IRequest;

public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteQuestionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Questions
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Question), request.Id);
        }

        _context.Questions.Remove(entity);

        entity.AddDomainEvent(new QuestionDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
