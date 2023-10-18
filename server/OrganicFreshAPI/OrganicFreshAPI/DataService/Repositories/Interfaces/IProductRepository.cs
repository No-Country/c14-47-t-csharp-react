using OrganicFreshAPI.Entities.Dtos;
using OrganicFreshAPI.Entities.Dtos.Requests;
using OrganicFreshAPI.Entities.Dtos.Responses;

namespace OrganicFreshAPI.DataService.Repositories.Interfaces;

public interface IProductRepository
{
    Task<ResultDto<GetProductsResponse>> GetProducts();
    Task<ResultDto<UpdateProductResponse>> UpdateProduct(int productId, UpdateProductRequest request);
    Task<ResultDto<CreateProductResponse>> CreateProduct(CreateProductRequest request);
    Task<ResultDto<object>> DeleteProduct(int productId);
}