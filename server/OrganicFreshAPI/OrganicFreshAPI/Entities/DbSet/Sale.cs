namespace OrganicFreshAPI.Entities.DbSet;

public class Sale
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public ICollection<CheckoutDetails> CheckoutsDetails { get; set; } = new List<CheckoutDetails>();
    public string PaymentId { get; set; } = string.Empty;
    public bool ConfirmedPayment { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    public DateTime DeletedAt { get; set; } = DateTime.UtcNow;

}