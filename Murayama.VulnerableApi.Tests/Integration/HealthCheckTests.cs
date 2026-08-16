using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Murayama.VulnerableApi;
using Murayama.VulnerableApi.Tests.Infrastructure;

namespace Murayama.VulnerableApi.Tests.Integration;

public class HealthCheckTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public HealthCheckTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Application_Should_Start_And_Respond()
    {
        var response = await _client.GetAsync("/swagger/index.html");

        Assert.True(
            response.StatusCode is HttpStatusCode.OK
                or HttpStatusCode.NotFound);
    }
}