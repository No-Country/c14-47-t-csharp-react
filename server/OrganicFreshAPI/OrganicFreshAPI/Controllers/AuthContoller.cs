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

        return Ok(result.Response);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authRepository.Login(request);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Response);
    }
}