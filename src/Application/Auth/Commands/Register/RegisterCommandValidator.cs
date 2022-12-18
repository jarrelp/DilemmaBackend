using CleanArchitecture.Application.Common.CustomValidators;
using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    private readonly IIdentityService _identityService;

    public RegisterCommandValidator(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public RegisterCommandValidator()
    {
        RuleFor(v => v.UserName)
            .NotNullOrEmpty()
            .NotStartWithWhiteSpace()
            .NotEndWithWhiteSpace()
            .MaximumLength(200).WithMessage("UserName must not exceed 200 characters.")
            .MustAsync(BeUniqueUsername).WithMessage("The specified username already exists.");
        RuleFor(v => v.Password)
            .NotNullOrEmpty()
            .NotStartWithWhiteSpace()
            .NotEndWithWhiteSpace()
            .MaximumLength(200).WithMessage("UserName must not exceed 200 characters.");

        RuleFor(v => v.ConfirmPassword)
            .NotNullOrEmpty()
            .Equal(v => v.Password);
    }

    public async Task<bool> BeUniqueUsername(string userName, CancellationToken cancellationToken)
    {
        var allUsers = await _identityService.GetAllUsersAsync();
        return allUsers.All(x => x.UserName != userName);
    }
}
