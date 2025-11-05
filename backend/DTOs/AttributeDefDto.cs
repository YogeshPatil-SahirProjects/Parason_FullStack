namespace Parason_Api.DTOs
{
    public class AttributeDefDto
    {
        public int AttributeID { get; set; }
        public string AttributeCode { get; set; } = string.Empty;
        public string AttributeName { get; set; } = string.Empty;
        public string DataType { get; set; } = string.Empty;
        public string? UnitDefault { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public List<AttributeListValueDto>? ListValues { get; set; }
    }
}
