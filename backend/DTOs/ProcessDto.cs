namespace Parason_Api.DTOs
{
    public class ProcessDto
    {
        public int ProcessID { get; set; }
        public string ProcessCode { get; set; } = string.Empty;
        public string ProcessName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
