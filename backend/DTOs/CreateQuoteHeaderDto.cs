namespace Parason_Api.DTOs
{
    public class CreateQuoteHeaderDto
    {
        public string QuoteNumber { get; set; } = string.Empty;
        public string QuoteName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string Status { get; set; } = "Draft";
        public string Currency { get; set; } = "INR";
        public int ValidityDays { get; set; } = 30;
        public string? Notes { get; set; }
    }
}
