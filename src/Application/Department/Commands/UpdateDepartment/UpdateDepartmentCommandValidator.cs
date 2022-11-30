using CleanArchitecture.Application.Common.CustomValidators;
using FluentValidation;

namespace CleanArchitecture.Application.Departments.Commands.UpdateDepartment;

public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotNullOrEmpty()
            .NotStartWithWhiteSpace()
            .NotEndWithWhiteSpace()
            .MaximumLength(50);
    }
}
