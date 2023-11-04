using System.Runtime.CompilerServices;
using ErrorOr;
using OrganicFreshAPI.Entities.DbSet;
using OrganicFreshAPI.Entities.Dtos;
using OrganicFreshAPI.Entities.Dtos.Requests;
using OrganicFreshAPI.Entities.Dtos.Responses;

namespace OrganicFreshAPI.DataService.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<ErrorOr<List<Category>>> GetCategories();
    Task<ErrorOr<GetProductsFromCategoryResponse>> GetProductsFromCategory(int categoryId);
    Task<ErrorOr<CreateCategoryResponse>> CreateCategory(CreateCategoryRequest request);
    Task<ErrorOr<UpdateCategoryResponse>> UpdateCategory(int categoryId, UpdateCategoryRequest request);
    Task<ErrorOr<bool>> DeleteCategory(int categoryId);
}