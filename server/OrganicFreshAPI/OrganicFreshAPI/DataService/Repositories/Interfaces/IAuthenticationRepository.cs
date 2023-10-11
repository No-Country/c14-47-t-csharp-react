using OrganicFreshAPI.Entities.Dtos;
using OrganicFreshAPI.Entities.Dtos.Requests;

namespace OrganicFreshAPI.DataService.Repositories.Interfaces;

public interface IAuthenticationRepository
{
    Task<ResultDto<string>> Register(RegisterRequest request);
    Task<ResultDto<string>> Login(LoginRequest request);
    bool VerifyJwt(string userId);
}
