using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Murayama.VulnerableApi.Data;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/vulnerable/orders")]
[Authorize]
public class VulnerableOrdersController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public VulnerableOrdersController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);
        
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
        if (page < 1)
            page = 1;

        if (pageSize < 1)
            pageSize = 20;

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
            count = orders.Count,
            data = orders
        });
    }
}