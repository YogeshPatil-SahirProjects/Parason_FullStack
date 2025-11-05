namespace Parason_Api.DTOs
{
    public class PriceCalculationRequestDto
    {
        public int? ModelID { get; set; }
        public int? EquipmentID { get; set; }
        public List<int>? ItemIds { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
