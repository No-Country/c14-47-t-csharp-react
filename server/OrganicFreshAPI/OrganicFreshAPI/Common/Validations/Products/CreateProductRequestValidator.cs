using FluentValidation;
using OrganicFreshAPI.Entities.Dtos.Requests;

namespace OrganicFreshAPI.Validations;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Price).NotEmpty().GreaterThanOrEqualTo(0);

        RuleFor(x => x.CategoryId).NotEmpty();

        RuleFor(x => x.Image)
            .Must(BeAValidImage)
            .WithMessage("La imagen debe tener una extensión permitida (ex., JPG, PNG)");

        RuleFor(x => x.Image)
            .Must(BeAValidImageSize)
            .WithMessage("La imagen excede el maximo permitido (e.g., 5MB)");

        RuleFor(x => x.WeightUnit)
            .NotEmpty()
            .Must(BeAValidWeightUnit)
            .WithMessage("Unidades permitidas: Oz or Lb");

        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0);
    }

    private bool BeAValidImage(IFormFile image)
    {
        if (image == null)
            return true;

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var fileExtension = Path.GetExtension(image.FileName).ToLower();
        return allowedExtensions.Contains(fileExtension);
    }

    private bool BeAValidImageSize(IFormFile image)
    {
        if (image == null)
            return true;

        const long maxFileSize = 5 * 1024 * 1024; // 5MB

        return image.Length <= maxFileSize;
    }

    private bool BeAValidWeightUnit(string unit)
    {
        if (string.IsNullOrEmpty(unit)) return true;
        return unit.ToLower() == "oz" || unit.ToLower() == "lb";
    }
}