namespace Parason_Api.DTOs
{
    public class CreateSeriesDto
    {
        public int EquipmentID { get; set; }
        public string SeriesCode { get; set; } = string.Empty;
        public string SeriesName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
