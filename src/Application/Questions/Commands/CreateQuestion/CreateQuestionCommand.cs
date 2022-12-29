using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Question;
using MediatR;

namespace CleanArchitecture.Application.Questions.Commands.CreateQuestion;

public record CreateQuestionCommand() : IRequest<QuestionDto>
{
    public int QuizId { get; init; }
    public string Description { get; init; } = null!;
}

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, QuestionDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateQuestionCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QuestionDto> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var entity = new Question
        {
            QuizId = request.QuizId,
            Description = request.Description
        };

        entity.AddDomainEvent(new QuestionCreatedEvent(entity));

        _context.Questions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<QuestionDto>(entity);

        return result;
    }
}
