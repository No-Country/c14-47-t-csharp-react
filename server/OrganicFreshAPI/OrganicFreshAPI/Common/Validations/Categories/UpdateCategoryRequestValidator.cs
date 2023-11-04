using FluentValidation;
using OrganicFreshAPI.Entities.Dtos.Requests;

namespace OrganicFreshAPI.Validations.Categories;

public class UpdateProductRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.image)
            .Must(BeAValidImage)
            .WithMessage("La imagen debe tener una extensión permitida (ex., JPG, PNG)");

        RuleFor(x => x.image)
            .Must(BeAValidImageSize)
            .WithMessage("La imagen excede el maximo permitido (e.g., 5MB)");
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

}