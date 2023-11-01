namespace OrganicFreshAPI.Entities.DbSet;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public bool? Active { get; set; } = true;
    public string WeightUnit { get; set; } = "Oz";
    public decimal Stock { get; set; } = 0;
    public string ImageUrl { get; set; } = string.Empty;
    public string PublicId { get; set; } = string.Empty;
    public ICollection<CheckoutDetails> CheckoutsDetails { get; set; } = new List<CheckoutDetails>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(-3);
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow.AddHours(-3);
    public DateTime DeletedAt { get; set; } = DateTime.UtcNow.AddHours(-3);
}