using CleanArchitecture.Application.Common.CustomValidators;
using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.OptionSkills.Commands.UpdateOptionSkill;

public class UpdateOptionSkillCommandValidator : AbstractValidator<UpdateOptionSkillCommand>
{
    /*private readonly IApplicationDbContext _context;

    public UpdateOptionSkillCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Description)
            *//*.when(x => !string.IsNullOrEmpty(x.Description))
            .NotNullOrEmpty()
            .NotStartWithWhiteSpace()
            .NotEndWithWhiteSpace()*//*
            .MaximumLength(200).WithMessage("Description must not exceed 200 characters.")
            .MustAsync(BeUniqueDescription).WithMessage("The specified description already exists.");
    }

    public async Task<bool> BeUniqueDescription(string description, CancellationToken cancellationToken)
    {
        return await _context.OptionSkills
            .AllAsync(l => l.Description != description, cancellationToken);
    }*/
}

