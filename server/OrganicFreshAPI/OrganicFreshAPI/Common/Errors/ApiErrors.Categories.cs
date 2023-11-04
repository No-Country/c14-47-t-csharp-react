using ErrorOr;

namespace OrganicFreshAPI.Common.Errors;

public static partial class ApiErrors
{
    public static class Category
    {
        public static Error InvalidCategory => Error.Validation(
            code: "Product.InvalidCategory",
            description: "Invalid Category"
        );

        public static Error InvalidProduct = Error.Validation(
            code: "Product.InvalidProductID",
            description: "The product does not exists"
        );

        public static Error HasActiveProducts = Error.Failure(
            code: "Category.HasActiveProducts",
            description: "The category has active products therefore cannot be deleted"
        );
    }
}