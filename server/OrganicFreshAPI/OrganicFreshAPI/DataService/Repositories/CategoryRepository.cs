using Microsoft.EntityFrameworkCore;
using OrganicFreshAPI.DataService.Data;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.DbSet;
using OrganicFreshAPI.Entities.Dtos;
using OrganicFreshAPI.Entities.Dtos.Requests;
using OrganicFreshAPI.Entities.Dtos.Responses;

namespace OrganicFreshAPI.DataService.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly MyDbContext _context;

    public CategoryRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<CreateCategoryResponse>> CreateCategory(CreateCategoryRequest request)
    {
        if (request.name is null)
            return new ResultDto<CreateCategoryResponse>()
            {
                IsSuccess = false,
                Message = "The category name is required"
            };

        var newCategory = new Category
        {
            Name = request.name,
            ImageUrl = request.imageUrl == null ? "" : request.imageUrl
        };
        await _context.Categories.AddAsync(newCategory);
        var saved = await _context.SaveChangesAsync();
        return new ResultDto<CreateCategoryResponse>
        {
            IsSuccess = saved > 0,
            Message = saved > 0 ? "Category created successfully" : "Something went wrong while creating the category",
            Response = new CreateCategoryResponse(newCategory),
        };
    }


    public async Task<ResultDto<List<Category>>> GetCategories()
    {
        return new ResultDto<List<Category>>
        {
            IsSuccess = true,
            Response = await _context.Categories.ToListAsync()
        };
    }

    public async Task<ResultDto<UpdateCategoryResponse>> UpdateCategory(int categoryId, UpdateCategoryRequest request)
    {
        var categoryToUpdate = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        if (categoryToUpdate is null)
            return new ResultDto<UpdateCategoryResponse>
            {
                IsSuccess = false,
                Message = "The category does not exists",
            };
        categoryToUpdate.Name = request.name == null ? categoryToUpdate.Name : request.name;
        categoryToUpdate.ImageUrl = request.imageUrl == null ? categoryToUpdate.ImageUrl : request.imageUrl;

        var saved = await _context.SaveChangesAsync();
        return new ResultDto<UpdateCategoryResponse>
        {
            IsSuccess = saved > 0,
            Message = saved > 0 ? "Updated successfully" : "Something went wrong while updating",
            Response = new UpdateCategoryResponse(categoryToUpdate)
        };
    }

    public async Task<ResultDto<object>> DeleteCategory(int categoryId)
    {
        var categoryToDelete = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        if (categoryToDelete is null)
            return new ResultDto<object>
            {
                IsSuccess = false,
                Message = "The category does not exists"
            };
        _context.Categories.Remove(categoryToDelete);
        var saved = await _context.SaveChangesAsync();
        return new ResultDto<object>
        {
            IsSuccess = saved > 0,
            Message = saved > 0 ? "Deleted Successfully" : "Something went wrong while deleting",
        };
    }
}
