using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Quizzes.Commands.UpdateQuiz;

public record UpdateQuizCommand : IRequest<QuizDto>
{
    public int Id { get; init; }

    public string Description { get; init; } = null!;
}

public class UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand, QuizDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateQuizCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<QuizDto> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Quizzes
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Quiz), request.Id);
        }

        entity.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<QuizDto>(entity);

        return result;
    }
}
