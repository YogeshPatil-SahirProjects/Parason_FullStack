namespace Parason_Api.DTOs
{
    public class CreateSpecDetailsDto
    {
        public int RecordID { get; set; }
        public int? EquipmentID { get; set; }
        public int? ModelID { get; set; }
        public int AttributeID { get; set; }
        public int? ListValueID { get; set; }
        public decimal? NumValue { get; set; }
        public string? TextValue { get; set; }
        public bool? BoolValue { get; set; }
    }
}
