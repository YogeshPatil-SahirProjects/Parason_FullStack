namespace Parason_Api.DTOs
{
    public class UpdateModelDto
    {
        public int SeriesID { get; set; }
        public string ModelCode { get; set; } = string.Empty;
        public string ModelName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
