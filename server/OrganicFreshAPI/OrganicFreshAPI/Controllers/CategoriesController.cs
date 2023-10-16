using Microsoft.AspNetCore.Mvc;
using OrganicFreshAPI.DataService.Repositories.Interfaces;

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
}