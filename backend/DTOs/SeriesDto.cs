namespace Parason_Api.DTOs
{
    public class SeriesDto
    {
        public int SeriesID { get; set; }
        public int EquipmentID { get; set; }
        public string SeriesCode { get; set; } = string.Empty;
        public string SeriesName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string? EquipmentName { get; set; }
        public List<ModelDto>? Models { get; set; }
    }
}
