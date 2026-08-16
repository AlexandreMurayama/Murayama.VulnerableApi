using System.Net;

namespace Murayama.VulnerableApi.Tests.Infrastructure;

public class RecordingHttpMessageHandler : HttpMessageHandler
{
    public Uri? RequestedUri { get; private set; }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        RequestedUri = request.RequestUri;

        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("internal-service-response")
        };

        return Task.FromResult(response);
    }
}