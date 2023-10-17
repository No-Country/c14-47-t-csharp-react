using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.Dtos.Requests;

namespace OrganicFreshAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
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

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Response);
    }

    [Authorize(Policy = "ElevatedRights")]
    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
    {
        var result = await _categoryRepository.CreateCategory(request);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Response);
    }
    [Authorize(Policy = "ElevatedRights")]
    [HttpPut("{categoryId}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] int categoryId, [FromBody] UpdateCategoryRequest request)
    {
        var result = await _categoryRepository.UpdateCategory(categoryId, request);
        if (!result.IsSuccess && result.Message == "Category does not exists")
            return NotFound();
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Response);
    }


    [Authorize(Policy = "ElevatedRights")]
    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
    {
        var result = await _categoryRepository.DeleteCategory(categoryId);
        if (!result.IsSuccess && result.Message == "Category does not exists")
            return NotFound();
        if (!result.IsSuccess)
            return BadRequest(result.Message);


        return Ok(result.Response);
    }
}