namespace OrganicFreshAPI;

public static class Consts
{
    public const int PasswordMinLength = 5;

    public const string PasswordRegex =
    @"^(?=.*[A-Z])(?=.*\d)[a-zA-Z0-9]{4,}$";

    public const string PasswordMinLengthValidationError = "Password must have more than 5 characters.";
    public const string EmailValidationError = "Email must have valid format.";

    public const string PasswordValidationError =
        "Password must have more than 5 characters, min. 1 uppercase, min. 1 lowercase.";
}
