namespace Parason_Api.DTOs
{
    public class CreateScopeOfSupplyDto
    {
        public int RecordID { get; set; }
        public int? ModelID { get; set; }
        public int ItemId { get; set; }
        public decimal? Price_INR { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
