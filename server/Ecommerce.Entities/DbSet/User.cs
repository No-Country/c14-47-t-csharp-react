using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Entities.DbSet;

public class User : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime ModifiedAt { get; set; } = DateTime.Now;
    public DateTime DeletedAt { get; set; } = DateTime.Now;
}