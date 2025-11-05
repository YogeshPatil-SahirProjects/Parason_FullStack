namespace Parason_Api.DTOs
{
    public class EquipmentDto
    {
        public int EquipmentID { get; set; }
        public string EquipmentCode { get; set; } = string.Empty;
        public string EquipmentName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public List<SeriesDto>? Series { get; set; }
    }
}
