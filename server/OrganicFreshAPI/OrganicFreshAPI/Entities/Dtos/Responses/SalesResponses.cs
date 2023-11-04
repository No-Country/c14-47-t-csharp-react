using OrganicFreshAPI.Entities.DbSet;

namespace OrganicFreshAPI.Entities.Dtos.Responses;

public record GetSaleResponse(
    string userId,
    int productId,
    string productName,
    decimal quantity,
    decimal total,
    DateTime createdAt
);

public record GetSalesResponse(
    List<Sale> sales
);

public record SaleAndCheckoutDetails
{
    public int SaleId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public bool ConfirmedPayment { get; set; }
    public List<CheckoutDetails> CheckoutDetails { get; set; }
}


public record SaleAndCheckoutDetailsAdmin
{
    public int SaleId { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public bool ConfirmedPayment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public List<CheckoutDetails> CheckoutDetails { get; set; }
}