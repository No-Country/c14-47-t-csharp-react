using System.Runtime.CompilerServices;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using OrganicFreshAPI.Common.Errors;
using OrganicFreshAPI.DataService.Data;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.DbSet;

namespace OrganicFreshAPI.DataService.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly MyDbContext _context;

    public SaleRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Sale>> CreateSale(string userId)
    {
        var newSale = new Sale
        {
            UserId = userId,
        };

        await _context.Sales.AddAsync(newSale);
        var saved = await _context.SaveChangesAsync();

        if (saved <= 0)
            return CommonErrors.DbSaveError;

        return newSale;
    }

    public async Task<ErrorOr<bool>> CreateCheckoutDetails(int saleId, int productId, decimal quantity)
    {
        var sale = await _context.Sales.FirstOrDefaultAsync(s => s.Id == saleId);
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        var checkoutDetails = new CheckoutDetails
        {
            SaleId = saleId,
            Sale = sale,
            ProductId = productId,
            Product = product
        };

        sale.CheckoutsDetails.Add(checkoutDetails);

        var saved = await _context.SaveChangesAsync();

        if (saved <= 0)
            return CommonErrors.DbSaveError;

        return true;
    }
}
