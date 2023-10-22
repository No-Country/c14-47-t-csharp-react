using ErrorOr;
using OrganicFreshAPI.Entities.Dtos;
using OrganicFreshAPI.Entities.Dtos.Requests;
using OrganicFreshAPI.Entities.Dtos.Responses;

namespace OrganicFreshAPI.DataService.Repositories.Interfaces;

public interface IProductRepository
{
    Task<ErrorOr<GetProductsResponse>> GetProducts();
    Task<ErrorOr<GetProductResponse>> UpdateProduct(int productId, UpdateProductRequest request);
    Task<ErrorOr<GetProductResponse>> CreateProduct(CreateProductRequest request);
    Task<ErrorOr<bool>> DeleteProduct(int productId);
}