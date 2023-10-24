namespace OrganicFreshAPI.Entities.DbSet;

public class CheckoutDetails
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int SaleId { get; set; }
    public Sale Sale { get; set; }
    public decimal Quantity { get; set; }
    public decimal Total { get; set; }
}