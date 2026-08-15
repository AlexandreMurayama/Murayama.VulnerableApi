using Microsoft.EntityFrameworkCore;
using Murayama.VulnerableApi.Models;

namespace Murayama.VulnerableApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    
    public DbSet<CouponRedemption> CouponRedemptions => Set<CouponRedemption>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CouponRedemption>()
            .HasIndex(r => new { r.UserId, r.CouponCode })
            .IsUnique();
    }
}