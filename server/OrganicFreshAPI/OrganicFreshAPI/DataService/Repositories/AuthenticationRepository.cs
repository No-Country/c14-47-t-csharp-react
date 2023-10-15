using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.DbSet;
using OrganicFreshAPI.Entities.Dtos;
using OrganicFreshAPI.Entities.Dtos.Requests;

namespace OrganicFreshAPI.DataService.Repositories;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    public AuthenticationRepository(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<ResultDto<string>> Register(RegisterRequest request)
    {
        var userByEmail = await _userManager.FindByEmailAsync(request.Email);
        if (userByEmail is not null)
        {
            return new ResultDto<string> { IsSuccess = false, Message = "User already exists" };
        }


        User user = new()
        {
            Email = request.Email,
            Name = request.Name,
            UserName = request.Email,
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        await _userManager.AddToRoleAsync(user, Role.User);

        if (!result.Succeeded)
        {
            return new ResultDto<string>
            {
                IsSuccess = false,
                Message =
            $"Something went wrong {GetErrorsText(result.Errors)}"
            };
        }


        return await Login(new LoginRequest { Email = request.Email, Password = request.Password });
    }

    public async Task<ResultDto<string>> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return new ResultDto<string> { IsSuccess = false, Message = "Password or Email is incorrect" };
        }

        var authClaims = new List<Claim>
        {
            new("Name", user.Name),
            new("Email", user.Email),
            new("Id", user.Id)
        };

        var userRoles = await _userManager.GetRolesAsync(user);

        authClaims.AddRange(userRoles.Select(userRole => new Claim("Role", userRole)));

        bool isAdmin = userRoles.Contains(Role.Admin);

        var token = GetToken(authClaims);

        return new ResultDto<string> { IsSuccess = true, Message = "Login successful", Response = new JwtSecurityTokenHandler().WriteToken(token), isAdmin = isAdmin };
    }

    public async Task<ResultDto<UserDetailsResponse>> UserDetails(IHttpContextAccessor contextAccessor)
    {
        var user = contextAccessor.HttpContext.User;
        var userId = user.FindFirst("Id")?.Value;
        User userInfo = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        var response = new UserDetailsResponse(userInfo.Name, userInfo.CreatedAt, userInfo.ModifiedAt, userInfo.DeletedAt, userInfo.Id, userInfo.Email);
        return new ResultDto<UserDetailsResponse> { IsSuccess = true, Response = response };
    }


    public bool VerifyJwt(string userId)
    {
        throw new NotImplementedException();
    }

    private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            expires: DateTime.Now.AddHours(15),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

        return token;
    }
    private string GetErrorsText(IEnumerable<IdentityError> errors)
    {
        return string.Join(",", errors.Select(error => error.Description).ToArray());
    }
}