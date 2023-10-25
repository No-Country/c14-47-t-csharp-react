using ErrorOr;
using OrganicFreshAPI.Entities.DbSet;

namespace OrganicFreshAPI.DataService.Repositories.Interfaces;

public interface ISaleRepository
{
    Task<ErrorOr<Sale>> CreateSale(string userId);
    Task<ErrorOr<bool>> CreateCheckoutDetails(int saleId, int productId, decimal quantity);
}