using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Quiz;
using MediatR;

namespace CleanArchitecture.Application.Quizzes.Commands.CreateQuiz;

public record CreateQuizCommand : IRequest<int>
{
    public string Description { get; init; } = null!;
}

public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateQuizCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
    {
        var entity = new Quiz();

        entity.Description = request.Description;

        entity.AddDomainEvent(new QuizCreatedEvent(entity));

        _context.Quizzes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
