using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Question;
using MediatR;

namespace CleanArchitecture.Application.Questions.Commands.CreateQuestion;

public record CreateQuestionCommand : IRequest<int>
{
    public int QuizId { get; set; }
    public string Description { get; init; } = null!;
}

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateQuestionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var entity = new Question();

        entity.QuizId = request.QuizId;
        entity.Description = request.Description;

        entity.AddDomainEvent(new QuestionCreatedEvent(entity));

        _context.Questions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
