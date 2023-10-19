using Microsoft.EntityFrameworkCore;
using OrganicFreshAPI.DataService.Data;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.DbSet;
using OrganicFreshAPI.Entities.Dtos;
using OrganicFreshAPI.Entities.Dtos.Requests;
using OrganicFreshAPI.Entities.Dtos.Responses;
using OrganicFreshAPI.Helpers;

namespace OrganicFreshAPI.DataService.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly MyDbContext _context;
    private readonly IImageService _imageService;

    public CategoryRepository(MyDbContext context, IImageService imageService)
    {
        _context = context;
        _imageService = imageService;
    }

    public async Task<ResultDto<CreateCategoryResponse>> CreateCategory(CreateCategoryRequest request)
    {
        if (request.name is null)
            return new ResultDto<CreateCategoryResponse>()
            {
                IsSuccess = false,
                Message = "The category name is required"
            };
        if (request.image == null)
        {
            return new ResultDto<CreateCategoryResponse>()
            {
                IsSuccess = false,
                Message = "The image is required"
            };
        }
        var resultImage = await _imageService.AddImageAsync(request.image);

        if (resultImage.Error != null)
            return new ResultDto<CreateCategoryResponse>()
            {
                IsSuccess = false,
                Message = $"Something went wrong while uploading the image {resultImage.Error.Message}",
            };

        var newCategory = new Category
        {
            Name = request.name,
            ImageUrl = resultImage.Url.ToString(),
            PublicId = resultImage.PublicId
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

    public async Task<ResultDto<UpdateCategoryResponse>> UpdateCategory(int categoryId, UpdateCategoryRequest request)
    {
        var categoryToUpdate = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

        if (categoryToUpdate is null)
            return new ResultDto<UpdateCategoryResponse>
            {
                IsSuccess = false,
                Message = "The category does not exists",
            };

        if (request.name == null && request.image == null)
            return new ResultDto<UpdateCategoryResponse>()
            {
                IsSuccess = true,
                Message = "There is nothing to update",
                Response = new UpdateCategoryResponse(categoryToUpdate)
            };

        categoryToUpdate.Name = request.name == null ? categoryToUpdate.Name : request.name;

        if (request.image != null)
        {
            var resultImage = await _imageService.AddImageAsync(request.image);
            var imageToDelete = await _imageService.DeleteImageAsync(categoryToUpdate.PublicId);
            if (resultImage.Error != null || imageToDelete.Error != null)
            {
                return new ResultDto<UpdateCategoryResponse>
                {
                    IsSuccess = false,
                    Message = $"Something went wrong while updating the image {resultImage.Error.Message ?? imageToDelete.Error.Message}",
                };
            }

            categoryToUpdate.ImageUrl = resultImage.Url.ToString();
            categoryToUpdate.PublicId = resultImage.PublicId;
        }

        var saved = await _context.SaveChangesAsync();
        return new ResultDto<UpdateCategoryResponse>
        {
            IsSuccess = saved > 0,
            Message = saved > 0 ? "Updated successfully" : "Something went wrong while updating, there are nothing to update",
            Response = new UpdateCategoryResponse(categoryToUpdate)
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

    public async Task<ResultDto<GetProductsFromCategoryResponse>> GetProductsFromCategory(int categoryId)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category is null)
        {
            return new ResultDto<GetProductsFromCategoryResponse>
            {
                IsSuccess = false,
                Message = "The category does not exists"
            };
        }

        var products = await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();

        return new ResultDto<GetProductsFromCategoryResponse>
        {
            IsSuccess = true,
            Message = "Query Successful",
            Response = new GetProductsFromCategoryResponse(category, products)
        };
    }
}
