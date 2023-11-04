using AutoMapper;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using OrganicFreshAPI.Common.Errors;
using OrganicFreshAPI.DataService.Data;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.DbSet;
using OrganicFreshAPI.Entities.Dtos.Requests;
using OrganicFreshAPI.Entities.Dtos.Responses;
using OrganicFreshAPI.Helpers;

namespace OrganicFreshAPI.DataService.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MyDbContext _context;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    public ProductRepository(MyDbContext context, IImageService imageService, IMapper mapper)
    {
        _context = context;
        _imageService = imageService;
        _mapper = mapper;
    }

    public async Task<ErrorOr<GetProductResponse>> CreateProduct(CreateProductRequest request)
    {
        CloudinaryDotNet.Actions.ImageUploadResult resultImage = null;

        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == request.CategoryId);
        if (category is null)
        {
            return ApiErrors.Product.InvalidCategory;
        }

        if (request.Image != null)
        {
            resultImage = await _imageService.AddImageAsync(request.Image);

            if (resultImage.Error != null)
                return CommonErrors.ImageUploadError;
        }

        Product newProduct = new Product
        {
            Name = request.Name,
            Price = request.Price,
            CategoryId = request.CategoryId,
            Active = request.Active ?? true,
            WeightUnit = request.WeightUnit ?? "Oz",
            Stock = request.Stock ?? 0,
            ImageUrl = resultImage != null ? resultImage.Url.ToString() : "",
            PublicId = resultImage != null ? resultImage.PublicId : "",
            Category = category
        };

        await _context.Products.AddAsync(newProduct);
        var saved = await _context.SaveChangesAsync();

        var response = _mapper.Map<GetProductResponse>(newProduct);
        if (saved <= 0)
            return CommonErrors.DbSaveError;

        return response;
    }

    public async Task<ErrorOr<bool>> DeleteProduct(int productId)
    {
        var productToDelete = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (productToDelete is null)
            return ApiErrors.Product.InvalidProduct;

        productToDelete.Active = false;

        var saved = await _context.SaveChangesAsync();

        if (saved <= 0)
            return CommonErrors.DbSaveError;

        return true;
    }

    public async Task<ErrorOr<Product>> GetProductById(int productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (product is null)
            return ApiErrors.Product.InvalidProduct;

        return product;
    }

    public async Task<ErrorOr<GetProductsResponse>> GetProducts()
    {
        var products = await _context.Products.Include(x => x.Category).ToListAsync();
        var productResponses = _mapper.Map<List<GetProductResponse>>(products);
        return new GetProductsResponse(productResponses);
    }

    public async Task<ErrorOr<GetProductResponse>> UpdateProduct(int productId, UpdateProductRequest request)
    {
        CloudinaryDotNet.Actions.ImageUploadResult resultImage = null;
        CloudinaryDotNet.Actions.DeletionResult imageToDelete = null;

        var productToUpdate = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        if (productToUpdate is null)
            return ApiErrors.Product.InvalidProduct;

        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == request.categoryId);

        if (request.categoryId.HasValue && category is null)
            return ApiErrors.Product.InvalidCategory;

        productToUpdate.Name = request.name ?? productToUpdate.Name;
        productToUpdate.Active = request.active ?? true;
        productToUpdate.Price = request.price ?? productToUpdate.Price;
        productToUpdate.CategoryId = request.categoryId ?? productToUpdate.CategoryId;
        productToUpdate.WeightUnit = request.weightUnit ?? productToUpdate.WeightUnit;
        productToUpdate.Stock = request.stock ?? productToUpdate.Stock;


        if (request.image != null)
        {
            resultImage = await _imageService.AddImageAsync(request.image);
            imageToDelete = await _imageService.DeleteImageAsync(productToUpdate.PublicId);
            if (resultImage.Error != null)
                return CommonErrors.ImageUploadError;

            if (imageToDelete.Error != null)
                return CommonErrors.ImageDeleteError;
            productToUpdate.ImageUrl = resultImage.Url.ToString();
            productToUpdate.PublicId = resultImage.PublicId;
        }

        var saved = await _context.SaveChangesAsync();

        if (saved <= 0)
            return CommonErrors.DbSaveError;

        var response = _mapper.Map<GetProductResponse>(productToUpdate);

        return response;
    }

    public async Task<ErrorOr<bool>> UpdateProductStock(int productId, decimal quantity)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (product is null)
            return ApiErrors.Product.InvalidProduct;
        product.Stock += quantity;
        if (product.Stock < 0)
            return ApiErrors.Product.NotEnoughStock;

        var saved = await _context.SaveChangesAsync();

        if (saved <= 0)
            return CommonErrors.DbSaveError;

        return true;
    }
}
