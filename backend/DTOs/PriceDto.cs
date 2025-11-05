using System;

namespace Parason_Api.DTOs;

public class PriceDto
{
    public int PriceId { get; set; }
    public int? EquipmentId { get; set; }
    public int? ModelId { get; set; }
    public int? ItemId { get; set; }
    public decimal BasePriceInr { get; set; }
    public DateTime EffectiveFrom { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;

    // Navigation properties
    public string? EquipmentName { get; set; }
    public string? ModelName { get; set; }
    public string? ItemName { get; set; }
}

public class CreatePriceDto
{
    public int? EquipmentId { get; set; }
    public int? ModelId { get; set; }
    public int? ItemId { get; set; }
    public decimal BasePriceInr { get; set; }
    public DateTime? EffectiveFrom { get; set; }
}

public class UpdatePriceDto
{
    public decimal BasePriceInr { get; set; }
    public DateTime EffectiveFrom { get; set; }
}
