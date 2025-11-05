namespace Parason_Api.DTOs
{
    public class VerticalAreaDto
    {
        public int VerticalID { get; set; }
        public string VerticalCode { get; set; } = string.Empty;
        public string VerticalName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
