using Microsoft.EntityFrameworkCore;
using OrganicFreshAPI.DataService.Data;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.DbSet;
using OrganicFreshAPI.Entities.Dtos;
using OrganicFreshAPI.Entities.Dtos.Requests;
using OrganicFreshAPI.Entities.Dtos.Responses;

namespace OrganicFreshAPI.DataService.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MyDbContext _context;

    public ProductRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<CreateProductResponse>> CreateProduct(CreateProductRequest request)
    {
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
        Product newProduct = new Product
        {
            Name = request.Name,
            Price = request.Price,
            CategoryId = request.CategoryId,
            Active = request.Active ?? true,
            ImageUrl = request.ImageUrl == null ? "" : request.ImageUrl

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
        productToUpdate.ImageUrl = request.imageUrl ?? productToUpdate.ImageUrl;
        productToUpdate.Price = request.price ?? productToUpdate.Price;
        productToUpdate.CategoryId = request.categoryId ?? productToUpdate.CategoryId;

        var saved = await _context.SaveChangesAsync();
        return new ResultDto<UpdateProductResponse>
        {
            IsSuccess = saved > 0,
            Message = saved > 0 ? "Updated successfully" : "Something went wrong while updating",
            Response = new UpdateProductResponse(productToUpdate)
        };
    }
}
