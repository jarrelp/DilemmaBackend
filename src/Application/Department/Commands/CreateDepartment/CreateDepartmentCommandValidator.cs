using CleanArchitecture.Application.Common.CustomValidators;
using FluentValidation;

namespace CleanArchitecture.Application.Departments.Commands.CreateDepartment;

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotNullOrEmpty()
            .NotStartWithWhiteSpace()
            .NotEndWithWhiteSpace()
            .MaximumLength(50);
    }
}
