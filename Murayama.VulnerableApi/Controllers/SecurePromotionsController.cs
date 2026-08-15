using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Murayama.VulnerableApi.Data;
using Murayama.VulnerableApi.DTOs.Promotions;
using Murayama.VulnerableApi.Models;

namespace Murayama.VulnerableApi.Controllers;

[ApiController]
[Route("api/secure/promotions")]
[Authorize]
public class SecurePromotionsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public SecurePromotionsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("redeem")]
    public async Task<IActionResult> Redeem(RedeemCouponRequest request)
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

        const string normalizedCouponCode = "WELCOME10";

        var alreadyRedeemed = await _dbContext.CouponRedemptions
            .AnyAsync(r =>
                r.UserId == userId &&
                r.CouponCode == normalizedCouponCode);

        if (alreadyRedeemed)
        {
            return Conflict(new
            {
                message = "Coupon has already been redeemed."
            });
        }

        var redemption = new CouponRedemption
        {
            UserId = userId,
            CouponCode = normalizedCouponCode,
            DiscountAmount = 10.00m,
            RedeemedAt = DateTime.UtcNow
        };

        _dbContext.CouponRedemptions.Add(redemption);

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return Conflict(new
            {
                message = "Coupon has already been redeemed."
            });
        }

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