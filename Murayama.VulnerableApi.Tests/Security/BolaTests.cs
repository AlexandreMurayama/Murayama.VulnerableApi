using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Murayama.VulnerableApi.DTOs.Auth;
using Murayama.VulnerableApi.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Murayama.VulnerableApi.Data;

namespace Murayama.VulnerableApi.Tests.Security;

public class BolaTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public BolaTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Alice_Can_Access_Bobs_Order_Demonstrating_Bola()
    {
        using var scope = _factory.Services.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<AppDbContext>();

        var bob = await dbContext.Users
            .SingleAsync(u => u.Email == "bob@murayama.local");

        var bobsOrder = await dbContext.Orders
            .FirstAsync(o => o.UserId == bob.Id);
        
        var loginResponse = await _client.PostAsJsonAsync(
            "/api/Auth/login",
            new
            {
                Email = "alice@murayama.local",
                Password = "Alice123!"
            });

        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        var login = await loginResponse.Content
            .ReadFromJsonAsync<LoginResponse>();

        Assert.NotNull(login);
        Assert.False(string.IsNullOrWhiteSpace(login.AccessToken));

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                login.AccessToken);

        var response = await _client.GetAsync(
            $"/api/vulnerable/orders/{bobsOrder.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var order = await response.Content
            .ReadFromJsonAsync<OrderResponse>();

        Assert.NotNull(order);
        Assert.Equal(bob.Id, order.UserId);
    }
    
    private sealed class OrderResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}