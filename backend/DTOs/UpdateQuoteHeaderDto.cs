namespace Parason_Api.DTOs
{
    public class UpdateQuoteHeaderDto
    {
        public string QuoteName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string Status { get; set; } = "Draft";
        public string Currency { get; set; } = "INR";
        public int ValidityDays { get; set; }
        public string? Notes { get; set; }
    }
}
