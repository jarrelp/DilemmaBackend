using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;

namespace CleanArchitecture.Application.Common.CustomValidators;

public static class CustomValidators
{
    public static IRuleBuilderOptions<T, string> NotStartWithWhiteSpace<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(m => m != null && !m.StartsWith(" ")).WithMessage("'{PropertyName}' should not start with whitespace");
    }

    public static IRuleBuilderOptions<T, string> NotEndWithWhiteSpace<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(m => m != null && !m.EndsWith(" ")).WithMessage("'{PropertyName}' should not end with whitespace");
    }

    public static IRuleBuilderOptions<T, string> NotNullOrEmpty<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(m => !string.IsNullOrEmpty(m)).WithMessage("'{PropertyName}' should not be null or empty");
    }
}
