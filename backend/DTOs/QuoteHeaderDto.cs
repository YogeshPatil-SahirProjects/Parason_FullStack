namespace Parason_Api.DTOs
{
    public class QuoteHeaderDto
    {
        public int QuoteID { get; set; }
        public byte QuoteRevision { get; set; }
        public string QuoteNumber { get; set; } = string.Empty;
        public string QuoteName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string Status { get; set; } = "Draft";
        public string Currency { get; set; } = "INR";
        public int ValidityDays { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public List<QuoteVerticalDto>? QuoteVerticals { get; set; }
    }
}
