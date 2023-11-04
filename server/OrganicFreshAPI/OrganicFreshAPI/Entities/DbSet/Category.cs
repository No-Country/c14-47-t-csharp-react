namespace OrganicFreshAPI.Entities.DbSet;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string PublicId { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(-3);
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow.AddHours(-3);
    public DateTime DeletedAt { get; set; } = DateTime.UtcNow.AddHours(-3);
    public ICollection<Product> Products { get; set; } = new List<Product>();
}