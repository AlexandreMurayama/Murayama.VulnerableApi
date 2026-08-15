namespace Murayama.VulnerableApi.DTOs.External;

public class SupplierRiskResponse
{
    public string? Supplier { get; set; }

    public int RiskScore { get; set; }

    public bool Approved { get; set; }

    public string? Notes { get; set; }
}