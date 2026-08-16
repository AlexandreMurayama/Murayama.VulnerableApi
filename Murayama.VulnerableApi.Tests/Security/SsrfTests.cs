using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Murayama.VulnerableApi.DTOs.Auth;
using Murayama.VulnerableApi.Tests.Infrastructure;

namespace Murayama.VulnerableApi.Tests.Security;

public class SsrfTests
    : IClassFixture<SsrfWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly SsrfWebApplicationFactory _factory;

    public SsrfTests(SsrfWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Vulnerable_Fetch_Allows_Request_To_Loopback_Address_Demonstrating_Ssrf()
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

        // Act - attempt to make the server access a loopback address
        const string targetUrl =
            "http://127.0.0.1:9999/internal";

        var response = await _client.PostAsJsonAsync(
            "/api/vulnerable/fetch",
            new
            {
                Url = targetUrl
            });

        // Assert - vulnerable endpoint accepted and attempted the request
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Assert.NotNull(_factory.RecordingHandler.RequestedUri);

        Assert.Equal(
            targetUrl,
            _factory.RecordingHandler.RequestedUri.ToString());
    }
}