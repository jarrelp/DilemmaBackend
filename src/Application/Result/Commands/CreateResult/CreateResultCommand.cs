using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Result;
using MediatR;

namespace CleanArchitecture.Application.Results.Commands.CreateResult;

public record CreateResultCommand : IRequest<int>
{
    public int QuizId { get; set; }

    public string ApplicationUserId { get; set; } = null!;

    public IList<int> AnswerIds { get; init; } = new List<int>();
}

public class CreateResultCommandHandler : IRequestHandler<CreateResultCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateResultCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateResultCommand request, CancellationToken cancellationToken)
    {
        var answers = new List<Option>();
        foreach(var item in request.AnswerIds)
        {
            answers.Add(_context.Options.Where(x => x.Id == item).First());
        }

        var entity = new Result
        {
            QuizId = request.QuizId,
            ApplicationUserId = request.ApplicationUserId,
            Answers = answers
        };

        entity.AddDomainEvent(new ResultCreatedEvent(entity));

        _context.Results.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
