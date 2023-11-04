using OrganicFreshAPI.Entities.DbSet;

namespace OrganicFreshAPI.Entities.Dtos.Responses;

public record GetProductResponse(
    int id,
    string name,
    decimal price,
    int categoryId,
    string categoryName,
    bool active,
    string weightUnit,
    decimal stock,
    string imageUrl,
    string publicId,
    DateTime createdAt,
    DateTime modifiedAt,
    DateTime DeletedAt
);

public record GetProductsResponse(List<GetProductResponse> products);
public record GetProductByIdResponse(Product product);
public record CreateProductResponse(Product product);
public record UpdateProductResponse(Product updatedProduct);

