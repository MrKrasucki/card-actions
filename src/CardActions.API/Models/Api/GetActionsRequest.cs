using FluentValidation;

namespace CardActions.API.Models.Api;

internal sealed record GetActionsRequest(string UserId, string CardNumber);

internal class GetActionsRequestValidator : AbstractValidator<GetActionsRequest>
{
    public GetActionsRequestValidator()
    {
        RuleFor(a => a.UserId)
            .NotEmpty().WithMessage("UserID is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(a => a.CardNumber)
            .NotEmpty().WithMessage("Card Number is required.")
            .MaximumLength(100).WithMessage("Card Number cannot exceed 100 characters.");
    }
}