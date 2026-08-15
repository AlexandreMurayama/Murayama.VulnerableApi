using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Murayama.VulnerableApi.Data;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/secure/orders")]
[Authorize]
public class SecureOrdersController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public SecureOrdersController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userIdClaim =
            User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub");

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var order = await _dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o =>
                o.Id == id &&
                o.UserId == userId);
        // Correção da vulnerabilidade. Agora é verificado não apenas o id do pedido mas também se o usuário é dono do pedido.

        if (order is null)
            return NotFound();

        return Ok(new
        {
            order.Id,
            order.UserId,
            order.Total,
            order.Status,
            order.CreatedAt,
            Items = order.Items.Select(item => new
            {
                item.Id,
                item.ProductName,
                item.UnitPrice,
                item.Quantity
            })
        });
    }
    
    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        const int defaultPageSize = 20;
        const int maxPageSize = 50;

        if (page < 1)
            page = 1;

        if (pageSize < 1)
            pageSize = defaultPageSize;

        if (pageSize > maxPageSize)
            pageSize = maxPageSize;

        var orders = await _dbContext.Orders
            .AsNoTracking()
            .OrderBy(o => o.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(o => new
            {
                o.Id,
                o.UserId,
                o.Total,
                o.Status,
                o.CreatedAt,
                Items = o.Items.Select(item => new
                {
                    item.Id,
                    item.ProductName,
                    item.UnitPrice,
                    item.Quantity
                })
            })
            .ToListAsync();

        return Ok(new
        {
            page,
            pageSize,
            maxPageSize,
            count = orders.Count,
            data = orders
        });
    }
}