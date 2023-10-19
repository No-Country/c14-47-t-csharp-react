using Microsoft.EntityFrameworkCore;
using OrganicFreshAPI.DataService.Data;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.DbSet;
using OrganicFreshAPI.Entities.Dtos;
using OrganicFreshAPI.Entities.Dtos.Requests;
using OrganicFreshAPI.Entities.Dtos.Responses;
using OrganicFreshAPI.Helpers;

namespace OrganicFreshAPI.DataService.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MyDbContext _context;
    private readonly IImageService _imageService;

    public ProductRepository(MyDbContext context, IImageService imageService)
    {
        _context = context;
        _imageService = imageService;
    }

    public async Task<ResultDto<CreateProductResponse>> CreateProduct(CreateProductRequest request)
    {
        CloudinaryDotNet.Actions.ImageUploadResult resultImage = null;

        if (request.Name is null || request.CategoryId <= 0)
            return new ResultDto<CreateProductResponse>()
            {
                IsSuccess = false,
                Message = "Invalid Name or CategoryId"
            };

        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == request.CategoryId);
        if (category is null)
        {
            return new ResultDto<CreateProductResponse>
            {
                IsSuccess = false,
                Message = "category not found"
            };
        }

        if (request.Image != null)
        {
            resultImage = await _imageService.AddImageAsync(request.Image);
            if (resultImage.Error != null)
                return new ResultDto<CreateProductResponse>()
                {
                    IsSuccess = false,
                    Message = $"Something went wrong while uploading the image {resultImage.Error.Message}",
                };
        }

        Product newProduct = new Product
        {
            Name = request.Name,
            Price = request.Price,
            CategoryId = request.CategoryId,
            Active = request.Active ?? true,
            ImageUrl = resultImage != null ? resultImage.Url.ToString() : "",
            PublicId = resultImage != null ? resultImage.PublicId : "",
        };

        await _context.Products.AddAsync(newProduct);
        var saved = await _context.SaveChangesAsync();
        return new ResultDto<CreateProductResponse>
        {
            IsSuccess = saved > 0,
            Message = saved > 0 ? "Product created successfully" : "Something went wrong while creating the product",
            Response = new CreateProductResponse(newProduct),
        };
    }

    public async Task<ResultDto<object>> DeleteProduct(int productId)
    {
        var productToDelete = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (productToDelete is null)
            return new ResultDto<object>
            {
                IsSuccess = false,
                Message = "The product does not exists"
            };

        productToDelete.Active = false;

        var saved = await _context.SaveChangesAsync();
        return new ResultDto<object>
        {
            IsSuccess = saved > 0,
            Message = saved > 0 ? "Deleted successfully" : "Something went wrong while deleting",
        };
    }

    public async Task<ResultDto<GetProductsResponse>> GetProducts()
    {
        return new ResultDto<GetProductsResponse>
        {
            IsSuccess = true,
            Response = new GetProductsResponse(await _context.Products.ToListAsync())
        };
    }

    public async Task<ResultDto<UpdateProductResponse>> UpdateProduct(int productId, UpdateProductRequest request)
    {
        CloudinaryDotNet.Actions.ImageUploadResult resultImage = null;
        CloudinaryDotNet.Actions.DeletionResult imageToDelete = null;

        var productToUpdate = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        if (productToUpdate is null)
            return new ResultDto<UpdateProductResponse>
            {
                IsSuccess = false,
                Message = "The product does not exists"
            };

        if (request.categoryId.HasValue && request.categoryId <= 0)
        {
            return new ResultDto<UpdateProductResponse>
            {
                IsSuccess = false,
                Message = "Invalid category ID"
            };
        }
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == request.categoryId);
        if (request.categoryId.HasValue && category is null)
        {
            return new ResultDto<UpdateProductResponse>
            {
                IsSuccess = false,
                Message = "category not found"
            };
        }

        productToUpdate.Name = request.name ?? productToUpdate.Name;
        productToUpdate.Active = request.active ?? true;
        productToUpdate.Price = request.price ?? productToUpdate.Price;
        productToUpdate.CategoryId = request.categoryId ?? productToUpdate.CategoryId;

        if (request.image != null)
        {
            resultImage = await _imageService.AddImageAsync(request.image);
            imageToDelete = await _imageService.DeleteImageAsync(productToUpdate.PublicId);
            if (resultImage.Error != null || imageToDelete.Error != null)
            {
                return new ResultDto<UpdateProductResponse>
                {
                    IsSuccess = false,
                    Message = $"Something went wrong while updating the image {resultImage.Error.Message ?? imageToDelete.Error.Message}",
                };
            }

            productToUpdate.ImageUrl = resultImage.Url.ToString();
            productToUpdate.PublicId = resultImage.PublicId;
        }

        var saved = await _context.SaveChangesAsync();
        return new ResultDto<UpdateProductResponse>
        {
            IsSuccess = saved > 0,
            Message = saved > 0 ? "Updated successfully" : "Something went wrong while updating",
            Response = new UpdateProductResponse(productToUpdate)
        };
    }
}
