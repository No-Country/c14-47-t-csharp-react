using System.ComponentModel.DataAnnotations;
using OrganicFreshAPI.Entities.DbSet;

namespace OrganicFreshAPI.Entities.Dtos.Requests;

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public bool? Active = true;
    public IFormFile? Image { get; set; }
    public string? WeightUnit { get; set; }
    public int? Stock { get; set; }
}



public record UpdateProductRequest(
    string? name,
    decimal? price,
    bool? active,
    IFormFile? image,
    int? categoryId,
    string? weightUnit,
    int? stock
);

