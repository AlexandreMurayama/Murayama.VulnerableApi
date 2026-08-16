using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Murayama.VulnerableApi;

namespace Murayama.VulnerableApi.Tests.Infrastructure;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public CustomWebApplicationFactory()
    {
        Environment.SetEnvironmentVariable(
            "Jwt__Issuer",
            "Murayama.VulnerableApi.Tests");

        Environment.SetEnvironmentVariable(
            "Jwt__Audience",
            "Murayama.VulnerableApi.Tests");

        Environment.SetEnvironmentVariable(
            "Jwt__Key",
            "MurayamaVulnerableApi-Test-Key-Only-For-Automated-Tests-2026");

        Environment.SetEnvironmentVariable(
            "Jwt__ExpirationMinutes",
            "60");
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureAppConfiguration((context, config) =>
        {
            var testConnectionString =
                Environment.GetEnvironmentVariable("TEST_DB_CONNECTION")
                ?? throw new InvalidOperationException(
                    "Environment variable TEST_DB_CONNECTION is not configured.");

            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnection"] = testConnectionString
            });
        });
    }
}