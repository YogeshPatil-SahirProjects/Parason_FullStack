namespace Parason_Api.DTOs
{
    public class CatalogProcessDto
    {
        public int ProcessID { get; set; }
        public string ProcessCode { get; set; } = string.Empty;
        public string ProcessName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? SequenceNo { get; set; }
        public bool IsRequired { get; set; }
        public List<CatalogEquipmentDto>? Equipments { get; set; }
    }
}
