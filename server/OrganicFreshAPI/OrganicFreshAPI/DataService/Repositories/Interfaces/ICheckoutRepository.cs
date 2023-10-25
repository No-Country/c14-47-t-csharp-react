using ErrorOr;
using OrganicFreshAPI.Entities.Dtos.Requests;
using OrganicFreshAPI.Entities.Dtos.Responses;

namespace OrganicFreshAPI.DataService.Repositories.Interfaces;

public interface ICheckoutRepository
{
    Task<ErrorOr<CreateCheckoutResponse>> CreateCheckout(List<CreateCheckoutRequest> request);
}