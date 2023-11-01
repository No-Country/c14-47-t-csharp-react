using AutoMapper;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using OrganicFreshAPI.Common.Errors;
using OrganicFreshAPI.DataService.Data;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.DbSet;
using OrganicFreshAPI.Entities.Dtos.Responses;

namespace OrganicFreshAPI.DataService.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly MyDbContext _context;
    private readonly IMapper _mapper;
    public SaleRepository(MyDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ErrorOr<Sale>> CreateSale(string userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        var newSale = new Sale
        {
            UserId = userId,
            User = user
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
            SaleId = sale.Id,
            Sale = sale,
            ProductId = productId,
            ProductName = product.Name,
            Product = product,
            Quantity = quantity,
            Total = product.Price * quantity
        };

        sale.CheckoutsDetails.Add(checkoutDetails);

        var saved = await _context.SaveChangesAsync();

        if (saved <= 0)
            return CommonErrors.DbSaveError;

        return true;
    }

    public async Task<ErrorOr<List<SaleAndCheckoutDetails>>> GetSalesFromUser(string userId)
    {
        var sales = await _context.Sales.Where(s => s.ConfirmedPayment == true)
    .Where(s => s.UserId == userId)
    .Select(s => new SaleAndCheckoutDetails
    {
        SaleId = s.Id,
        CreatedAt = s.CreatedAt,
        ModifiedAt = s.ModifiedAt,
        DeletedAt = s.DeletedAt,
        ConfirmedPayment = s.ConfirmedPayment,
        CheckoutDetails = s.CheckoutsDetails
            .Select(ct => new CheckoutDetails
            {
                ProductId = ct.ProductId,
                SaleId = ct.SaleId,
                Quantity = ct.Quantity,
                Total = ct.Total,
                ProductName = ct.Product.Name,
            })
            .ToList()
    })
    .ToListAsync();

        return sales;
    }

    public async Task<ErrorOr<List<SaleAndCheckoutDetailsAdmin>>> GetAllSales()
    {
        var sales = await _context.Sales.Where(s => s.ConfirmedPayment == true)
    .Select(s => new SaleAndCheckoutDetailsAdmin
    {
        SaleId = s.Id,
        CreatedAt = s.CreatedAt,
        UserId = s.UserId,
        UserName = s.User.Name,
        UserEmail = s.User.Email,
        ConfirmedPayment = s.ConfirmedPayment,
        ModifiedAt = s.ModifiedAt,
        DeletedAt = s.DeletedAt,
        CheckoutDetails = s.CheckoutsDetails
            .Select(ct => new CheckoutDetails
            {
                ProductId = ct.ProductId,
                SaleId = ct.SaleId,
                Quantity = ct.Quantity,
                Total = ct.Total,
                ProductName = ct.Product.Name
            })
            .ToList()
    })
    .ToListAsync();

        return sales;
    }

    public async Task<ErrorOr<bool>> DeleteSale(string paymentId)
    {
        var saleToDelete = await _context.Sales.FirstOrDefaultAsync(s => s.PaymentId == paymentId);
        if (saleToDelete is null)
            return ApiErrors.Sale.InvalidPaymentId;

        _context.Sales.Remove(saleToDelete);

        var saved = await _context.SaveChangesAsync();

        if (saved <= 0)
            return CommonErrors.DbSaveError;

        return true;
    }

    public async Task<ErrorOr<SaleAndCheckoutDetails>> ConfirmPay(string paymentId)
    {
        var saleToUpdate = await _context.Sales.FirstOrDefaultAsync(s => s.PaymentId == paymentId);

        if (saleToUpdate is null)
            return ApiErrors.Sale.InvalidPaymentId;

        saleToUpdate.ConfirmedPayment = true;

        var saved = await _context.SaveChangesAsync();

        if (saved <= 0)
            return CommonErrors.DbSaveError;

        return new SaleAndCheckoutDetails
        {
            SaleId = saleToUpdate.Id,
            CreatedAt = saleToUpdate.CreatedAt,
            ModifiedAt = saleToUpdate.ModifiedAt,
            DeletedAt = saleToUpdate.DeletedAt,
            CheckoutDetails = saleToUpdate.CheckoutsDetails
                   .Select(ct => new CheckoutDetails
                   {
                       ProductId = ct.ProductId,
                       SaleId = ct.SaleId,
                       Quantity = ct.Quantity,
                       Total = ct.Total,
                       ProductName = ct.Product.Name
                   }).ToList()
        };
    }
}
