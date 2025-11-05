namespace Parason_Api.DTOs
{
    public class CatalogVerticalDto
    {
        public int VerticalID { get; set; }
        public string VerticalCode { get; set; } = string.Empty;
        public string VerticalName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<CatalogProcessDto>? Processes { get; set; }
    }
}
