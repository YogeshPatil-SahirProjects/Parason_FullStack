namespace Parason_Api.DTOs
{
    public class PriceCalculationResponseDto
    {
        public decimal? BasePrice { get; set; }
        public decimal ItemsTotal { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public string Currency { get; set; } = "INR";
        public List<PriceBreakdownDto>? ItemBreakdown { get; set; }
    }
}
