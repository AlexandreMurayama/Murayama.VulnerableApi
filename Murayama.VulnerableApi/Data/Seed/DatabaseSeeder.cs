using Microsoft.EntityFrameworkCore;
using Murayama.VulnerableApi.Models;
using Murayama.VulnerableApi.Services;

namespace Murayama.VulnerableApi.Data.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext dbContext, PasswordService passwordService)
    {
        if (await dbContext.Users.AnyAsync())
            return;

        var alice = new User
        {
            Name = "Alice",
            Email = "alice@murayama.local",
            PasswordHash = string.Empty,
            Role = "User"
        };

        var bob = new User
        {
            Name = "Bob",
            Email = "bob@murayama.local",
            PasswordHash = string.Empty,
            Role = "User"
        };

        var admin = new User
        {
            Name = "Admin",
            Email = "admin@murayama.local",
            PasswordHash = string.Empty,
            Role = "Admin"
        };

        alice.PasswordHash = passwordService.HashPassword(alice, "Alice123!");
        bob.PasswordHash = passwordService.HashPassword(bob, "Bob123!");
        admin.PasswordHash = passwordService.HashPassword(admin, "Admin123!");
        
        dbContext.Users.AddRange(alice, bob, admin);
        await dbContext.SaveChangesAsync();

        var orders = new[]
        {
            new Order
            {
                UserId = alice.Id,
                Total = 299.90m,
                Status = "Paid",
                Items =
                {
                    new OrderItem
                    {
                        ProductName = "Security Key",
                        UnitPrice = 299.90m,
                        Quantity = 1
                    }
                }
            },

            new Order
            {
                UserId = alice.Id,
                Total = 89.90m,
                Status = "Pending",
                Items =
                {
                    new OrderItem
                    {
                        ProductName = "USB-C Hub",
                        UnitPrice = 89.90m,
                        Quantity = 1
                    }
                }
            },

            new Order
            {
                UserId = bob.Id,
                Total = 499.90m,
                Status = "Paid",
                Items =
                {
                    new OrderItem
                    {
                        ProductName = "Mechanical Keyboard",
                        UnitPrice = 499.90m,
                        Quantity = 1
                    }
                }
            }
        };

        dbContext.Orders.AddRange(orders);
        await dbContext.SaveChangesAsync();
    }
}