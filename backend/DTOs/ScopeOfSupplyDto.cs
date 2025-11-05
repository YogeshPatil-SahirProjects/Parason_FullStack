namespace Parason_Api.DTOs;

public class ScopeOfSupplyDto
{
    public int RecordId { get; set; }
    public int? ModelId { get; set; }
    public int ItemId { get; set; }
    public decimal? PriceInr { get; set; }
    public int Quantity { get; set; }

    // Navigation properties
    public string? ItemName { get; set; }
    public string? ModelName { get; set; }
}

public class CreateScopeOfSupplyDto
{
    public int RecordId { get; set; }
    public int? ModelId { get; set; }
    public int ItemId { get; set; }
    public decimal? PriceInr { get; set; }
    public int Quantity { get; set; } = 1;
}

public class UpdateScopeOfSupplyDto
{
    public int? ModelId { get; set; }
    public decimal? PriceInr { get; set; }
    public int Quantity { get; set; }
}
