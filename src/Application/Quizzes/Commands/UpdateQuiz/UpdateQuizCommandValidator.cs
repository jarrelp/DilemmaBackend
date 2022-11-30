using CleanArchitecture.Application.Common.CustomValidators;
using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Quizzes.Commands.UpdateQuiz;

public class UpdateQuizCommandValidator : AbstractValidator<UpdateQuizCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateQuizCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Description)
            .NotNullOrEmpty()
            .NotStartWithWhiteSpace()
            .NotEndWithWhiteSpace()
            .MaximumLength(200).WithMessage("Description must not exceed 200 characters.")
            .MustAsync(BeUniqueDescription).WithMessage("The specified description already exists.");
    }

    public async Task<bool> BeUniqueDescription(UpdateQuizCommand model, string description, CancellationToken cancellationToken)
    {
        return await _context.Quizzes
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Description != description, cancellationToken);
    }
}
