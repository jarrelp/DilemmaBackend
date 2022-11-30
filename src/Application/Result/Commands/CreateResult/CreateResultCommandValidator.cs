using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Results.Commands.CreateResult;

public class CreateResultCommandValidator : AbstractValidator<CreateResultCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateResultCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.QuizId)
            .NotNull()
            .NotEmpty();
        RuleFor(v => v.ApplicationUserId)
            .NotNull()
            .NotEmpty();
        RuleFor(v => v.AnswerIds)
            .NotNull()
            .NotEmpty();
        /*RuleFor(v => v).MustAsync(BeUniqueResultForEachPersonAndQuiz).WithMessage("The specified description already exists.");*/
    }

    /*public async Task<bool> BeUniqueResultForEachPersonAndQuiz(CreateResultCommand createResultCommand, CancellationToken cancellationToken)
    {
        return await _context.Results
            .AllAsync(l => l.QuizId != createResultCommand.QuizId && l.ApplicationUserId != createResultCommand.ApplicationUserId, cancellationToken);
    }*/
}
