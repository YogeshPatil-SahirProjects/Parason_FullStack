namespace Parason_Api.DTOs
{
    public class ModelDto
    {
        public int ModelID { get; set; }
        public int SeriesID { get; set; }
        public string ModelCode { get; set; } = string.Empty;
        public string ModelName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string? SeriesName { get; set; }
        public decimal? BasePrice { get; set; }
    }
}
