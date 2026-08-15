namespace Murayama.VulnerableApi.Models;

public class CouponRedemption
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public required string CouponCode { get; set; }

    public decimal DiscountAmount { get; set; }

    public DateTime RedeemedAt { get; set; }

    public User User { get; set; } = null!;
}