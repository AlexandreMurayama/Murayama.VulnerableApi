namespace Murayama.VulnerableApi.Models;

public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public required string ProductName { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public Order Order { get; set; } = null!;
}