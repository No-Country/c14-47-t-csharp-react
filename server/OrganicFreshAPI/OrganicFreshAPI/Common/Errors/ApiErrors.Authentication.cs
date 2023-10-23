using ErrorOr;

namespace OrganicFreshAPI.Common.Errors;

public static partial class ApiErrors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Unauthorized(
            code: "Auth.InvalidCredentials",
            description: "Invalid credentials"
        );

        public static Error UserAlreadyExists => Error.Conflict(
            code: "Auth.UserAlreadyExists",
            description: "User already exists"
        );

        public static Error UserNotFoundInRequest = Error.Failure(
            code: "Auth.UserNotFoundInRequest",
            description: "User were not found in request"
        );
    };
}