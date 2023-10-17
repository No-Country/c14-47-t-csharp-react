using System.Runtime.CompilerServices;
using OrganicFreshAPI.Entities.DbSet;
using OrganicFreshAPI.Entities.Dtos;
using OrganicFreshAPI.Entities.Dtos.Requests;
using OrganicFreshAPI.Entities.Dtos.Responses;

namespace OrganicFreshAPI.DataService.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<ResultDto<List<Category>>> GetCategories();
    Task<ResultDto<CreateCategoryResponse>> CreateCategory(CreateCategoryRequest request);
    Task<ResultDto<UpdateCategoryResponse>> UpdateCategory(int categoryId, UpdateCategoryRequest request);
    Task<ResultDto<object>> DeleteCategory(int categoryId);
}