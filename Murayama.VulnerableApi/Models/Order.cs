namespace Murayama.VulnerableApi.Models;

public class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public decimal Total { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}