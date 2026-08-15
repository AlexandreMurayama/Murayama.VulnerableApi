using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Murayama.VulnerableApi.DTOs.Fetch;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/secure/fetch")]
[Authorize]
public class SecureFetchController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public SecureFetchController(
        IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public async Task<IActionResult> Fetch(FetchUrlRequest request)
    {
        if (!Uri.TryCreate(
                request.Url,
                UriKind.Absolute,
                out var uri))
        {
            return BadRequest(new
            {
                message = "Invalid URL."
            });
        }

        if (uri.Scheme != Uri.UriSchemeHttp &&
            uri.Scheme != Uri.UriSchemeHttps)
        {
            return BadRequest(new
            {
                message = "Only HTTP and HTTPS URLs are allowed."
            });
        }

        if (uri.IsLoopback)
        {
            return BadRequest(new
            {
                message = "Local and private destinations are not allowed."
            });
        }

        IPAddress[] addresses;

        try
        {
            addresses = await Dns.GetHostAddressesAsync(uri.Host);
        }
        catch
        {
            return BadRequest(new
            {
                message = "Unable to resolve destination."
            });
        }

        if (addresses.Any(IsPrivateOrLocalAddress))
        {
            return BadRequest(new
            {
                message = "Local and private destinations are not allowed."
            });
        }

        var client = _httpClientFactory.CreateClient();

        try
        {
            var response = await client.GetAsync(uri);

            var content =
                await response.Content.ReadAsStringAsync();

            return Ok(new
            {
                requestedUrl = uri.ToString(),
                statusCode = (int)response.StatusCode,
                content
            });
        }
        catch (HttpRequestException)
        {
            return BadRequest(new
            {
                message = "Unable to fetch the requested URL."
            });
        }
    }

    private static bool IsPrivateOrLocalAddress(IPAddress address)
    {
        if (IPAddress.IsLoopback(address))
            return true;

        if (address.AddressFamily ==
            System.Net.Sockets.AddressFamily.InterNetwork)
        {
            var bytes = address.GetAddressBytes();

            // 10.0.0.0/8
            if (bytes[0] == 10)
                return true;

            // 172.16.0.0/12
            if (bytes[0] == 172 &&
                bytes[1] >= 16 &&
                bytes[1] <= 31)
                return true;

            // 192.168.0.0/16
            if (bytes[0] == 192 &&
                bytes[1] == 168)
                return true;

            // 169.254.0.0/16 - link-local
            if (bytes[0] == 169 &&
                bytes[1] == 254)
                return true;

            // 0.0.0.0/8
            if (bytes[0] == 0)
                return true;
        }

        if (address.AddressFamily ==
            System.Net.Sockets.AddressFamily.InterNetworkV6)
        {
            if (address.IsIPv6LinkLocal ||
                address.IsIPv6SiteLocal)
            {
                return true;
            }

            var bytes = address.GetAddressBytes();

            // fc00::/7 - IPv6 Unique Local Addresses
            if ((bytes[0] & 0xFE) == 0xFC)
                return true;
        }

        return false;
    }
}   