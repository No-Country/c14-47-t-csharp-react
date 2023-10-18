using OrganicFreshAPI.Entities.DbSet;

namespace OrganicFreshAPI.Entities.Dtos.Responses;

public record GetProductsResponse(List<Product> products);
public record GetProductByIdResponse(Product product);
public record CreateProductResponse(Product product);
public record UpdateProductResponse(Product updatedProduct);

