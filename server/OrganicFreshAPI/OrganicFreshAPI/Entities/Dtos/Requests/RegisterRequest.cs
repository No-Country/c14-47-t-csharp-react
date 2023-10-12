using System.ComponentModel.DataAnnotations;

namespace OrganicFreshAPI.Entities.Dtos.Requests;

public class RegisterRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    [EmailAddress(ErrorMessage = Consts.EmailValidationError)]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MinLength(Consts.PasswordMinLength, ErrorMessage = Consts.PasswordMinLengthValidationError)]
    [RegularExpression(Consts.PasswordRegex, ErrorMessage = Consts.PasswordValidationError)]
    public string Password { get; set; } = string.Empty;
}