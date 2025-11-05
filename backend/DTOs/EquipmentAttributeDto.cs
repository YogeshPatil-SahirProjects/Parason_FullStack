namespace Parason_Api.DTOs
{
    public class EquipmentAttributeDto
    {
        public int AttributeID { get; set; }
        public string AttributeName { get; set; } = string.Empty;
        public string DataType { get; set; } = string.Empty;
        public string? Unit { get; set; }
        public bool IsEditable { get; set; }
        public bool IsRequired { get; set; }
        public string? AttributeCategory { get; set; }
        public decimal? NumValue { get; set; }
        public string? TextValue { get; set; }
        public bool? BoolValue { get; set; }

        // For list-based attributes
        public string? ListValue { get; set; }
        public List<AttributeListValueDto>? ListOptions { get; set; }
    }
}
