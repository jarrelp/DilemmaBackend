using CleanArchitecture.Application.Common.CustomValidators;
using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Skills.Commands.UpdateSkill;

public class UpdateSkillCommandValidator : AbstractValidator<UpdateSkillCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateSkillCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
            .NotNullOrEmpty()
            .NotStartWithWhiteSpace()
            .NotEndWithWhiteSpace()
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
            .MustAsync(BeUniqueName).WithMessage("The specified Name already exists.");
    }

    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return await _context.Skills
            .AllAsync(l => l.Name != name, cancellationToken);
    }
}
