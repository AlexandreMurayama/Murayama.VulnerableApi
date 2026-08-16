using System.Net;
using System.Net.Http.Json;
using Murayama.VulnerableApi.Tests.Infrastructure;

namespace Murayama.VulnerableApi.Tests.Security;

public class BrokenAuthenticationTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public BrokenAuthenticationTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Vulnerable_Login_Allows_Repeated_Failed_Attempts_Without_Rate_Limiting()
    {
        for (var attempt = 1; attempt <= 5; attempt++)
        {
            var response = await _client.PostAsJsonAsync(
                "/api/vulnerable/auth/login",
                new
                {
                    Email = "alice@murayama.local",
                    Password = "WrongPassword!"
                });

            Assert.Equal(
                HttpStatusCode.Unauthorized,
                response.StatusCode);
            
            Assert.True(
                response.StatusCode != HttpStatusCode.TooManyRequests,
                $"Attempt {attempt} was rate limited unexpectedly.");
        }
    }
}