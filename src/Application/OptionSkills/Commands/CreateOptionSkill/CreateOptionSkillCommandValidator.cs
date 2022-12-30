using CleanArchitecture.Application.Common.CustomValidators;
using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.OptionSkills.Commands.CreateOptionSkill;

public class CreateOptionSkillCommandValidator : AbstractValidator<CreateOptionSkillCommand>
{
    /*private readonly IApplicationDbContext _context;

    public CreateOptionSkillCommandValidator(IApplicationDbContext context)
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
        return await _context.OptionSkills
            .AllAsync(l => l.Description != description, cancellationToken);
    }*/
}

