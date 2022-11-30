using FluentValidation;

namespace CleanArchitecture.Application.Questions.Queries.GetQuestionsWithPagination;

public class GetQuestionsWithPaginationQueryValidator : AbstractValidator<GetQuestionsWithPaginationQuery>
{
    public GetQuestionsWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
