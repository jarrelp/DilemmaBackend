using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Quiz;
using MediatR;

namespace CleanArchitecture.Application.Quizzes.Commands.CreateQuiz;

public record CreateQuizCommand : IRequest<QuizDto>
{
    public string Description { get; init; } = null!;
}

public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, QuizDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateQuizCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QuizDto> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
    {
        var entity = new Quiz
        {
            Description = request.Description
        };

        entity.AddDomainEvent(new QuizCreatedEvent(entity));

        _context.Quizzes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<QuizDto>(entity);

        return result;
    }
}
