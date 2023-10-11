using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrganicFreshAPI.Entities.DbSet;

namespace OrganicFreshAPI.DataService.Data;
public class MyDbContext : IdentityDbContext<User>
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {

    }
}