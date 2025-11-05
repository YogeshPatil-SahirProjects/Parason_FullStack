namespace Parason_Api.DTOs
{
    public class CatalogEquipmentDto
    {
        public int EquipmentID { get; set; }
        public string EquipmentCode { get; set; } = string.Empty;
        public string EquipmentName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? SequenceNo { get; set; }
        public bool IsRequired { get; set; }
    }
}
