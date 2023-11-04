using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrganicFreshAPI.Entities.DbSet;

namespace OrganicFreshAPI.DataService.Data;
public class MyDbContext : IdentityDbContext<User>
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<CheckoutDetails> CheckoutsDetails { get; set; }
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CheckoutDetails>()
        .HasKey(pd => new { pd.ProductId, pd.SaleId });

        modelBuilder.Entity<CheckoutDetails>()
        .HasOne(pd => pd.Product)
        .WithMany(p => p.CheckoutsDetails)
        .HasForeignKey(pd => pd.ProductId);

        modelBuilder.Entity<CheckoutDetails>()
            .HasOne(pd => pd.Sale)
            .WithMany(s => s.CheckoutsDetails)
            .HasForeignKey(pd => pd.SaleId);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Stock)
                .HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<CheckoutDetails>(entity =>
                {
                    entity.Property(e => e.Quantity)
                        .HasColumnType("decimal(18,2)");
                    entity.Property(e => e.Total)
                        .HasColumnType("decimal(18,2)");
                });
    }
}