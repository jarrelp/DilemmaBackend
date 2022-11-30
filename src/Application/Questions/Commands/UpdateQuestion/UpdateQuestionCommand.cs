using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Questions.Commands.UpdateQuestion;

public record UpdateQuestionCommand : IRequest
{
    public int Id { get; init; }
    public string Description { get; init; } = null!;
}

public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateQuestionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Questions
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Question), request.Id);
        }

        entity.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
