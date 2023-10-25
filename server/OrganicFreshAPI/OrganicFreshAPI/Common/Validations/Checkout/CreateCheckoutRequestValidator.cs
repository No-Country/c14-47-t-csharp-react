using FluentValidation;
using OrganicFreshAPI.Entities.Dtos.Requests;

namespace OrganicFreshAPI.Common.Validations.Checkout;

public class CreateCheckoutRequestValidator : AbstractValidator<CreateCheckoutRequest>
{
    public CreateCheckoutRequestValidator()
    {
        RuleFor(x => x.productId)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
        RuleFor(x => x.quantity)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}