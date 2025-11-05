namespace Parason_Api.DTOs;

public class SpecDetailDto
{
    public int RecordId { get; set; }
    public int? EquipmentId { get; set; }
    public int? ModelId { get; set; }
    public int AttributeId { get; set; }
    public int? ListValueId { get; set; }
    public decimal? NumValue { get; set; }
    public string? TextValue { get; set; }
    public bool? BoolValue { get; set; }

    // Navigation properties
    public string? AttributeName { get; set; }
    public string? EquipmentName { get; set; }
    public string? ModelName { get; set; }
    public string? ListValueDisplay { get; set; }
}

public class CreateSpecDetailDto
{
    public int RecordId { get; set; }
    public int? EquipmentId { get; set; }
    public int? ModelId { get; set; }
    public int AttributeId { get; set; }
    public int? ListValueId { get; set; }
    public decimal? NumValue { get; set; }
    public string? TextValue { get; set; }
    public bool? BoolValue { get; set; }
}

public class UpdateSpecDetailDto
{
    public int? EquipmentId { get; set; }
    public int? ModelId { get; set; }
    public int? ListValueId { get; set; }
    public decimal? NumValue { get; set; }
    public string? TextValue { get; set; }
    public bool? BoolValue { get; set; }
}
