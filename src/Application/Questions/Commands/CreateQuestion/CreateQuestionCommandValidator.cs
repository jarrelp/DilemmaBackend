using CleanArchitecture.Application.Common.CustomValidators;
using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Questions.Commands.CreateQuestion;

public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateQuestionCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Description)
            .NotNullOrEmpty()
            .NotStartWithWhiteSpace()
            .NotEndWithWhiteSpace()
            .MaximumLength(200).WithMessage("Description must not exceed 200 characters.")
            .MustAsync(BeUniqueDescription).WithMessage("The specified description already exists.");
    }

    public async Task<bool> BeUniqueDescription(string description, CancellationToken cancellationToken)
    {
        return await _context.Questions
            .AllAsync(l => l.Description != description, cancellationToken);
    }
}
