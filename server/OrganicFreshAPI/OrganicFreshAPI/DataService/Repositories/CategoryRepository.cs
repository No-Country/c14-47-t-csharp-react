using AutoMapper;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using OrganicFreshAPI.Common.Errors;
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
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    public CategoryRepository(MyDbContext context, IImageService imageService, IMapper mapper)
    {
        _context = context;
        _imageService = imageService;
        _mapper = mapper;
    }

    public async Task<ErrorOr<CreateCategoryResponse>> CreateCategory(CreateCategoryRequest request)
    {
        CloudinaryDotNet.Actions.ImageUploadResult resultImage = null;

        if (request.image != null)
        {

            resultImage = await _imageService.AddImageAsync(request.image);

            if (resultImage.Error != null)
                return CommonErrors.ImageUploadError;
        }

        var newCategory = new Category
        {
            Name = request.name,
            ImageUrl = resultImage != null ? resultImage.Url.ToString() : "",
            PublicId = resultImage != null ? resultImage.PublicId : ""
        };

        await _context.Categories.AddAsync(newCategory);
        var saved = await _context.SaveChangesAsync();

        if (saved <= 0)
            return CommonErrors.DbSaveError;


        return new CreateCategoryResponse(newCategory);
    }

    public async Task<ErrorOr<UpdateCategoryResponse>> UpdateCategory(int categoryId, UpdateCategoryRequest request)
    {
        var categoryToUpdate = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

        if (categoryToUpdate is null)
            return ApiErrors.Category.InvalidCategory;

        categoryToUpdate.Name = request.name == null ? categoryToUpdate.Name : request.name;

        if (request.image != null)
        {
            var resultImage = await _imageService.AddImageAsync(request.image);
            var imageToDelete = await _imageService.DeleteImageAsync(categoryToUpdate.PublicId);
            if (resultImage.Error != null || imageToDelete.Error != null)
            {
                return resultImage.Error != null ? CommonErrors.ImageUploadError : CommonErrors.ImageDeleteError;
            }

            categoryToUpdate.ImageUrl = resultImage.Url.ToString();
            categoryToUpdate.PublicId = resultImage.PublicId;
        }

        var saved = await _context.SaveChangesAsync();

        if (saved <= 0)
            return CommonErrors.DbSaveError;

        return new UpdateCategoryResponse(categoryToUpdate);
    }

    public async Task<ErrorOr<List<Category>>> GetCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<ErrorOr<bool>> DeleteCategory(int categoryId)
    {
        var categoryToDelete = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        if (categoryToDelete is null)
            return ApiErrors.Category.InvalidCategory;

        var hasActiveProducts = _context.Products.Where(p => p.CategoryId == categoryToDelete.Id).All(p => p.Active == false);

        if (hasActiveProducts)
            return ApiErrors.Category.HasActiveProducts;

        categoryToDelete.Active = false;
        // Change this to update active field in products
        var saved = await _context.SaveChangesAsync();

        if (saved <= 0)
            return CommonErrors.DbSaveError;

        return true;
    }

    public async Task<ErrorOr<GetProductsFromCategoryResponse>> GetProductsFromCategory(int categoryId)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category is null)
            return ApiErrors.Category.InvalidCategory;

        var products = await _context.Products.Where(p => p.CategoryId == categoryId).Select(p => _mapper.Map<GetProductResponse>(p)).ToListAsync();

        return new GetProductsFromCategoryResponse(products);
    }
}
