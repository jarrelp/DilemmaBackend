using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Quizzes.Commands.UpdateQuiz;

public record UpdateQuizCommand : IRequest
{
    public int Id { get; init; }

    public string Description { get; init; } = null!;
}

public class UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateQuizCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Quizzes
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Quiz), request.Id);
        }

        entity.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
