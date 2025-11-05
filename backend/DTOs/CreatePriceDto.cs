namespace Parason_Api.DTOs
{
    public class CreatePriceDto
    {
        public int? EquipmentID { get; set; }
        public int? ModelID { get; set; }
        public int? ItemId { get; set; }
        public decimal BasePriceINR { get; set; }
        public DateTime EffectiveFrom { get; set; }
    }
}
