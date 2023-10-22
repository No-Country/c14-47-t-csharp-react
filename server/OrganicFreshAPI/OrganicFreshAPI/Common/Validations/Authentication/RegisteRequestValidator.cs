using FluentValidation;
using OrganicFreshAPI.Entities.Dtos.Requests;

namespace OrganicFreshAPI.Validations.Authentication;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(Consts.PasswordRegex)
            .WithMessage("La contraseña tiene que tener una longitud mínima de 5 caracteres y contener al menos 1 mayúscula y 1 minúscula.");

        RuleFor(x => x.Password)
            .Length(8, 50);
    }
}