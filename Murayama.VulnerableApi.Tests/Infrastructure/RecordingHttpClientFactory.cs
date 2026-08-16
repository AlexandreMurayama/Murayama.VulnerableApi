namespace Murayama.VulnerableApi.Tests.Infrastructure;

public class RecordingHttpClientFactory : IHttpClientFactory
{
    private readonly RecordingHttpMessageHandler _handler;

    public RecordingHttpClientFactory(
        RecordingHttpMessageHandler handler)
    {
        _handler = handler;
    }

    public HttpClient CreateClient(string name)
    {
        return new HttpClient(_handler, disposeHandler: false);
    }
}