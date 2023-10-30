using ErrorOr;
using OrganicFreshAPI.Entities.Dtos.Requests;
using OrganicFreshAPI.Entities.Dtos.Responses;

namespace OrganicFreshAPI.DataService.Repositories.Interfaces;

public interface IAuthenticationRepository
{
    Task<ErrorOr<LoginResponse>> Register(RegisterRequest request);
    Task<ErrorOr<LoginResponse>> Login(LoginRequest request);
    Task<ErrorOr<UserDetailsResponse>> UserDetails();
}
