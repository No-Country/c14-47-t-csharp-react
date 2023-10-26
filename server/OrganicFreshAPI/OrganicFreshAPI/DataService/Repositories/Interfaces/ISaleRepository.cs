using ErrorOr;
using OrganicFreshAPI.Entities.DbSet;
using OrganicFreshAPI.Entities.Dtos.Responses;

namespace OrganicFreshAPI.DataService.Repositories.Interfaces;

public interface ISaleRepository
{
    Task<ErrorOr<Sale>> CreateSale(string userId);
    Task<ErrorOr<bool>> CreateCheckoutDetails(int saleId, int productId, decimal quantity);
    Task<ErrorOr<List<SaleAndCheckoutDetails>>> GetSalesFromUser(
        string userId);
    Task<ErrorOr<List<SaleAndCheckoutDetailsAdmin>>> GetAllSales();
}