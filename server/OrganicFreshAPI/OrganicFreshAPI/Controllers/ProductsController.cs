using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.Dtos.Requests;

namespace OrganicFreshAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
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

        if (!result.IsSuccess)
            return BadRequest();

        return Ok(result.Response);
    }

    [Authorize(Policy = "ElevatedRights")]
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequest request)
    {
        var result = await _productRepository.CreateProduct(request);
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Response);
    }

    [Authorize(Policy = "ElevatedRights")]
    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] int productId, [FromForm] UpdateProductRequest request)
    {
        var result = await _productRepository.UpdateProduct(productId, request);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Response);
    }

    [Authorize(Policy = "ElevatedRights")]
    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
    {
        var result = await _productRepository.DeleteProduct(productId);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }

}