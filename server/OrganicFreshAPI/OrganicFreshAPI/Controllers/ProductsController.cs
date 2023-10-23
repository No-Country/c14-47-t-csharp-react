using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganicFreshAPI.Common.Errors;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.Dtos.Requests;
using OrganicFreshAPI.Entities.Dtos.Responses;

namespace OrganicFreshAPI.Controllers;

[Route("[controller]")]
public class ProductsController : ApiController
{
    private readonly IProductRepository _productRepository;
    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var result = await _productRepository.GetProducts();

        return Ok(result.Value);
    }

    // [Authorize(Policy = "ElevatedRights")]
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequest request)
    {


        ErrorOr<GetProductResponse> result = await _productRepository.CreateProduct(request);

        if (result.IsError && result.FirstError == ApiErrors.Product.InvalidCategory)
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: result.FirstError.Description
            );

        if (result.IsError && result.FirstError == CommonErrors.DbSaveError)
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: result.FirstError.Description
            );

        return Ok(result.Value);
    }

    // [Authorize(Policy = "ElevatedRights")]
    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] int productId, [FromForm] UpdateProductRequest request)
    {
        var result = await _productRepository.UpdateProduct(productId, request);

        if (result.IsError && result.FirstError == ApiErrors.Product.InvalidCategory)
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: result.FirstError.Description
            );

        if (result.IsError && result.FirstError == ApiErrors.Product.InvalidProduct)
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: result.FirstError.Description
            );

        if (result.IsError && result.FirstError == CommonErrors.DbSaveError)
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: result.FirstError.Description
            );

        if (result.IsError && result.FirstError == CommonErrors.ImageUploadError)
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: result.FirstError.Description
            );

        return Ok(result.Value);
    }


    // [Authorize(Policy = "ElevatedRights")]
    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
    {
        var result = await _productRepository.DeleteProduct(productId);

        if (result.IsError && result.FirstError == ApiErrors.Product.InvalidProduct)
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: result.FirstError.Description
            );

        return NoContent();
    }
}