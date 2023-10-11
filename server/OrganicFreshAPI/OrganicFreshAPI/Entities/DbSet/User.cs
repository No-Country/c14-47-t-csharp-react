using Microsoft.AspNetCore.Identity;

namespace OrganicFreshAPI.Entities.DbSet;

public class User : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    public DateTime DeletedAt { get; set; } = DateTime.UtcNow;
}

