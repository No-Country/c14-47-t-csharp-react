using OrganicFreshAPI.Entities.DbSet;
using OrganicFreshAPI.Entities.Dtos;

namespace OrganicFreshAPI.DataService.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<ResultDto<List<Category>>> GetCategories();
}