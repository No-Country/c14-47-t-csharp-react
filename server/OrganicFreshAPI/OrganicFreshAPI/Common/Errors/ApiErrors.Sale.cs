using ErrorOr;

namespace OrganicFreshAPI.Common.Errors;

public static partial class ApiErrors
{
    public static class Sale
    {
        public static Error InvalidPaymentId => Error.Failure(
            code: "Sale.InvalidPaymentId",
            description: "Invalid PaymentId"
        );
    };
}