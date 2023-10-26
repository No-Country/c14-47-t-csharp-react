using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganicFreshAPI.Common.Errors;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.Dtos.Requests;

namespace OrganicFreshAPI.Controllers;

[Route("[controller]")]
public class CategoriesController : ApiController
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _categoryRepository.GetCategories();

        return Ok(result.Value);
    }

    [Authorize(Policy = "ElevatedRights")]
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryRequest request)
    {
        var result = await _categoryRepository.CreateCategory(request);

        if (result.IsError && result.FirstError == CommonErrors.ImageUploadError)
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: result.FirstError.Description
            );

        if (result.IsError && result.FirstError == CommonErrors.DbSaveError)
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: result.FirstError.Description
            );

        return Ok(result.Value);
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetProductsFromCategory([FromRoute] int categoryId)
    {
        var result = await _categoryRepository.GetProductsFromCategory(categoryId);

        if (result.IsError && result.FirstError == ApiErrors.Category.InvalidCategory)
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: result.FirstError.Description
            );

        return Ok(result.Value);
    }

    [Authorize(Policy = "ElevatedRights")]
    [HttpPut("{categoryId}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] int categoryId, [FromForm] UpdateCategoryRequest request)
    {
        var result = await _categoryRepository.UpdateCategory(categoryId, request);

        if (result.IsError && result.FirstError == ApiErrors.Category.InvalidCategory)
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


    [Authorize(Policy = "ElevatedRights")]
    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
    {
        var result = await _categoryRepository.DeleteCategory(categoryId);

        if (result.IsError && result.FirstError == ApiErrors.Category.InvalidProduct)
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: result.FirstError.Description
            );

        if (result.IsError && result.FirstError == ApiErrors.Category.HasActiveProducts)
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: result.FirstError.Description
            );

        if (result.IsError && result.FirstError == CommonErrors.DbSaveError)
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: result.FirstError.Description
            );

        return NoContent();
    }
}