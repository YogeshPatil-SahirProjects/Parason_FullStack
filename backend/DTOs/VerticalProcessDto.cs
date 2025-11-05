namespace Parason_Api.DTOs
{
    public class VerticalProcessDto
    {
        public int VerticalID { get; set; }
        public int ProcessID { get; set; }
        public int? SequenceNo { get; set; }
        public bool IsRequired { get; set; }
        public bool IsActive { get; set; }
        public string? VerticalName { get; set; }
        public string? ProcessName { get; set; }
    }
}
