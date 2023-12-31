using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrganicFreshAPI.Common.Errors;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.DbSet;
using OrganicFreshAPI.Entities.Dtos.Requests;
using OrganicFreshAPI.Entities.Dtos.Responses;

namespace OrganicFreshAPI.DataService.Repositories;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _ctxAccessor;
    private readonly ISaleRepository _saleRepository;

    private readonly IConfiguration _configuration;
    public AuthenticationRepository(UserManager<User> userManager, IConfiguration configuration, IHttpContextAccessor ctxAccessor, ISaleRepository saleRepository)
    {
        _userManager = userManager;
        _configuration = configuration;
        _ctxAccessor = ctxAccessor;
        _saleRepository = saleRepository;
    }

    public async Task<ErrorOr<LoginResponse>> Register(RegisterRequest request)
    {
        var userByEmail = await _userManager.FindByEmailAsync(request.Email);
        if (userByEmail is not null)
        {
            return ApiErrors.Authentication.UserAlreadyExists;
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
            return CommonErrors.DbSaveError;
        };

        return await Login(new LoginRequest(request.Email, request.Password));
    }

    public async Task<ErrorOr<LoginResponse>> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return ApiErrors.Authentication.InvalidCredentials;
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

        return new LoginResponse(new JwtSecurityTokenHandler().WriteToken(token), isAdmin);
    }

    public async Task<ErrorOr<UserDetailsResponse>> UserDetails()
    {
        var user = _ctxAccessor.HttpContext.User;

        var userId = user.FindFirst("Id")?.Value;
        if (userId is null)
        {
            return ApiErrors.Authentication.UserNotFoundInRequest;
        }
        else
        {
            User userInfo = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var sales = await _saleRepository.GetSalesFromUser(userId);
            if (sales.IsError)
                return CommonErrors.DbSaveError;

            return new UserDetailsResponse(
                userInfo.Name,
                userInfo.CreatedAt,
                userInfo.ModifiedAt,
                userInfo.DeletedAt,
                userInfo.Id,
                userInfo.Email,
                sales.Value);
        }
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