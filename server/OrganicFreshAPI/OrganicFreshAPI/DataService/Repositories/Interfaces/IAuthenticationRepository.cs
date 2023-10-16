using OrganicFreshAPI.Entities.Dtos;
using OrganicFreshAPI.Entities.Dtos.Requests;
using OrganicFreshAPI.Entities.Dtos.Responses;

namespace OrganicFreshAPI.DataService.Repositories.Interfaces;

public interface IAuthenticationRepository
{
    Task<ResultDto<string>> Register(RegisterRequest request);
    Task<ResultDto<string>> Login(LoginRequest request);
    Task<ResultDto<UserDetailsResponse>> UserDetails(IHttpContextAccessor contextAccessor);
    bool VerifyJwt(string userId);
}
