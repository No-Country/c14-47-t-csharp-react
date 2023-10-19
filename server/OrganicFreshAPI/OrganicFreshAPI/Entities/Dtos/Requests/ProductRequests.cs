using System.ComponentModel.DataAnnotations;

namespace OrganicFreshAPI.Entities.Dtos.Requests;

public class CreateProductRequest
{
    [Required(ErrorMessage = "The name is required")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "The price is required")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "The category id is required")]
    public int CategoryId { get; set; }
    public bool? Active = true;
    public IFormFile? Image { get; set; }
}



public record UpdateProductRequest(
    string? name,
    decimal? price,
    bool? active,
    IFormFile? image,
    int? categoryId
);

