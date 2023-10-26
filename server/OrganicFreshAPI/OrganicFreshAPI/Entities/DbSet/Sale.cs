namespace OrganicFreshAPI.Entities.DbSet;

public class Sale
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public ICollection<CheckoutDetails> CheckoutsDetails { get; set; } = new List<CheckoutDetails>();
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime DeletedAt { get; set; }

}