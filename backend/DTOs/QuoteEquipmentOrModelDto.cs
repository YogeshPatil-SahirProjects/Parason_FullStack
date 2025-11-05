namespace Parason_Api.DTOs;

public class QuoteEquipmentOrModelDto
{
    public int Qeomid { get; set; }
    public int RecordId { get; set; }
    public int? EquipmentId { get; set; }
    public int? SeriesId { get; set; }
    public int? ModelId { get; set; }
    public decimal? PriceInr { get; set; }
    public int Quantity { get; set; }

    // Navigation properties
    public string? EquipmentName { get; set; }
    public string? SeriesName { get; set; }
    public string? ModelName { get; set; }
}

public class CreateQuoteEquipmentOrModelDto
{
    public int RecordId { get; set; }
    public int? EquipmentId { get; set; }
    public int? SeriesId { get; set; }
    public int? ModelId { get; set; }
    public decimal? PriceInr { get; set; }
    public int Quantity { get; set; } = 1;
}

public class UpdateQuoteEquipmentOrModelDto
{
    public int RecordId { get; set; }
    public int? EquipmentId { get; set; }
    public int? SeriesId { get; set; }
    public int? ModelId { get; set; }
    public decimal? PriceInr { get; set; }
    public int Quantity { get; set; }
}
