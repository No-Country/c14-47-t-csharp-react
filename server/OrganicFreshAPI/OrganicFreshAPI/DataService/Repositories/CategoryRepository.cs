using Microsoft.EntityFrameworkCore;
using OrganicFreshAPI.DataService.Data;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.DbSet;
using OrganicFreshAPI.Entities.Dtos;

namespace OrganicFreshAPI.DataService.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly MyDbContext _context;

    public CategoryRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<List<Category>>> GetCategories()
    {
        return new ResultDto<List<Category>>
        {
            IsSuccess = true,
            Response = await _context.Categories.ToListAsync()
        };
    }
}
