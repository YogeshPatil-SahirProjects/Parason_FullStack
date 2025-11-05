namespace Parason_Api.DTOs
{
    public class CreateVerticalAreaDto
    {
        public string VerticalCode { get; set; } = string.Empty;
        public string VerticalName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
