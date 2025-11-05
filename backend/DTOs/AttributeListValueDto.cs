namespace Parason_Api.DTOs
{
    public class AttributeListValueDto
    {
        public int ListValueID { get; set; }
        public int AttributeID { get; set; }
        public string AttributeValue { get; set; } = string.Empty;
        public string? Display { get; set; }
        public int? SequenceNo { get; set; }
    }
}
