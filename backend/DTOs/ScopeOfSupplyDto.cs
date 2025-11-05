namespace Parason_Api.DTOs
{
    public class ScopeOfSupplyDto
    {
        public int RecordID { get; set; }
        public int? ModelID { get; set; }
        public int ItemId { get; set; }
        public decimal? Price_INR { get; set; }
        public int Quantity { get; set; }
        public string? ItemName { get; set; }
        public string? ItemCode { get; set; }
        public string? Description { get; set; }
        public bool IsMandatory { get; set; }
        public string? ModelName { get; set; }
    }
}
