using Microsoft.EntityFrameworkCore;
using Murayama.VulnerableApi.Models;

namespace Murayama.VulnerableApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
}