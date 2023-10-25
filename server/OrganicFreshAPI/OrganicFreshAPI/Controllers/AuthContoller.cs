using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganicFreshAPI.Common.Errors;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.Dtos.Requests;

namespace OrganicFreshAPI.Controllers;

[Route("[controller]")]
public class AuthController : ApiController
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

        if (result.IsError && result.FirstError == ApiErrors.Authentication.UserAlreadyExists)
            return Problem(
                statusCode: StatusCodes.Status409Conflict,
                title: result.FirstError.Description
            );

        var response = new
        {
            jwt = result.Value.jwt,
            isAdmin = result.Value.isAdmin
        };
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authRepository.Login(request);

        if (result.IsError && result.FirstError == ApiErrors.Authentication.InvalidCredentials)
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: result.FirstError.Description
            );

        var response = new
        {
            result.Value.jwt,
            result.Value.isAdmin
        };

        return Ok(response);
    }

    [Authorize(Policy = "StandardRights")]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var result = await _authRepository.UserDetails();
        if (result.IsError && result.FirstError == ApiErrors.Authentication.UserNotFoundInRequest)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: result.FirstError.Description
            );
        }
        return Ok(result.Value);
    }

    [Authorize(Policy = "ElevatedRights")]
    [HttpGet("me/admin")]
    public async Task<IActionResult> MeAdmin()
    {
        var result = await _authRepository.UserDetails();
        if (result.IsError && result.FirstError == ApiErrors.Authentication.UserNotFoundInRequest)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: result.FirstError.Description
            );
        }
        return Ok(result.Value);
    }
}