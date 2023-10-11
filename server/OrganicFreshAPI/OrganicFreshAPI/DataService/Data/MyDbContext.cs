using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrganicFreshAPI.Entities.DbSet;

namespace OrganicFreshAPI.DataService.Data;

public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

}