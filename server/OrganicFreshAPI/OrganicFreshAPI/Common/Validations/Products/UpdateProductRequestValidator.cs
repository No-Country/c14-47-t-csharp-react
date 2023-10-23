using FluentValidation;
using OrganicFreshAPI.Entities.Dtos.Requests;

namespace OrganicFreshAPI.Validations.Products;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.categoryId)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.image)
            .Must(BeAValidImage)
            .WithMessage("La imagen debe tener una extensión permitida (ex., JPG, PNG)");

        RuleFor(x => x.image)
            .Must(BeAValidImageSize)
            .WithMessage("La imagen excede el maximo permitido (e.g., 5MB)");

        RuleFor(x => x.weightUnit)
            .NotEmpty()
            .Must(BeAValidWeightUnit)
            .WithMessage("Unidades permitidas: Oz or Lb");

        RuleFor(x => x.stock).GreaterThanOrEqualTo(0);
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