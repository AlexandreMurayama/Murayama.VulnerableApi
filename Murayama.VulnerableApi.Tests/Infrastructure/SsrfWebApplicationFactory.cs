using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Murayama.VulnerableApi.Tests.Infrastructure;

public class SsrfWebApplicationFactory
    : CustomWebApplicationFactory
{
    public RecordingHttpMessageHandler RecordingHandler { get; } =
        new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IHttpClientFactory>();

            services.AddSingleton(RecordingHandler);

            services.AddSingleton<IHttpClientFactory>(
                serviceProvider =>
                    new RecordingHttpClientFactory(
                        serviceProvider.GetRequiredService<
                            RecordingHttpMessageHandler>()));
        });
    }
}