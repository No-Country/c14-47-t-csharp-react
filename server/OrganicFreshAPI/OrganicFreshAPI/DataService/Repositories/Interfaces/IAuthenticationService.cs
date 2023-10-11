using OrganicFreshAPI.Entities.Dtos.Requests;

namespace OrganicFreshAPI.DataService.Repositories.Interfaces;

public interface IAuthenticationRepository
{
    Task<string> Register(RegisterRequest request);
    Task<string> Login(LoginRequest request);
    bool VerifyJwt(string userId);
}
