using FluentValidation;
using OrganicFreshAPI.Entities.Dtos.Requests;

namespace OrganicFreshAPI.Validations.Authentication;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}