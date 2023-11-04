using ErrorOr;

namespace OrganicFreshAPI.Common.Errors;

public static partial class ApiErrors
{
    public static class Product
    {
        public static Error InvalidCategory => Error.Validation(
            code: "Product.InvalidCategory",
            description: "Invalid Category"
        );

        public static Error InvalidProduct = Error.Validation(
            code: "Product.InvalidProductID",
            description: "The product does not exists"
        );

        public static Error NotEnoughStock = Error.Validation(
            code: "Product.NotEnoughStock",
            description: "Not enough stock to fulfill the requested order"
        );
    }
}