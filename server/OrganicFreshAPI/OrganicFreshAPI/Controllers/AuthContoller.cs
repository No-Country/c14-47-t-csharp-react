using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.Dtos.Requests;

namespace OrganicFreshAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationRepository _authRepository;

    public AuthController(IAuthenticationRepository authRepository)
    {
        _authRepository = authRepository;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _authRepository.Register(request);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        var response = new
        {
            jwt = result.Response,
            result.isAdmin
        };
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authRepository.Login(request);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        var response = new
        {
            jwt = result.Response,
            result.isAdmin
        };

        return Ok(response);
    }

    [Authorize(Policy = "StandardRights")]
    [HttpGet("me")]
    public async Task<IActionResult> Me(IHttpContextAccessor contextAccessor)
    {
        var result = await _authRepository.UserDetails(contextAccessor);
        return Ok(result);
    }

    [Authorize(Policy = "ElevatedRights")]
    [HttpGet("me/admin")]
    public async Task<IActionResult> MeAdmin(IHttpContextAccessor contextAccessor)
    {
        var result = await _authRepository.UserDetails(contextAccessor);
        return Ok(result);
    }
}