namespace Parason_Api.DTOs
{
    public class CreateQuoteEquipmentOrModelDto
    {
        public int RecordID { get; set; }
        public int? EquipmentID { get; set; }
        public int? SeriesID { get; set; }
        public int? ModelID { get; set; }
        public decimal? Price_INR { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
