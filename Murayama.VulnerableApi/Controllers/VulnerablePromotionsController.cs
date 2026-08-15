using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Murayama.VulnerableApi.Data;
using Murayama.VulnerableApi.DTOs.Promotions;
using Murayama.VulnerableApi.Models;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/vulnerable/promotions")]
[Authorize]
public class VulnerablePromotionsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public VulnerablePromotionsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("redeem")]
    public async Task<IActionResult> Redeem(
        RedeemCouponRequest request)
    {
        var userIdClaim =
            User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub");

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        if (!string.Equals(
                request.CouponCode,
                "WELCOME10",
                StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest(new
            {
                message = "Invalid coupon."
            });
        }

        // Intentionally vulnerable:
        // no check is performed to determine whether this user
        // has already redeemed the coupon.
        var redemption = new CouponRedemption
        {
            UserId = userId,
            CouponCode = "WELCOME10",
            DiscountAmount = 10.00m,
            RedeemedAt = DateTime.UtcNow
        };

        _dbContext.CouponRedemptions.Add(redemption);

        await _dbContext.SaveChangesAsync();

        return Ok(new
        {
            message = "Coupon redeemed successfully.",
            redemption.Id,
            redemption.CouponCode,
            redemption.DiscountAmount,
            redemption.RedeemedAt
        });
    }
}