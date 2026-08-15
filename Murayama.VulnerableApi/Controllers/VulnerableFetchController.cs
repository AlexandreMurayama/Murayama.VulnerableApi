using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Murayama.VulnerableApi.DTOs.Fetch;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/vulnerable/fetch")]
[Authorize]
public class VulnerableFetchController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public VulnerableFetchController(
        IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public async Task<IActionResult> Fetch(
        FetchUrlRequest request)
    {
        var client = _httpClientFactory.CreateClient();

        try
        {
            var response = await client.GetAsync(request.Url);

            var content =
                await response.Content.ReadAsStringAsync();

            return Ok(new
            {
                requestedUrl = request.Url,
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
}