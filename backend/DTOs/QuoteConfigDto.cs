namespace Parason_Api.DTOs
{
    public class QuoteConfigDto
    {
        public VerticalAreaDto? Vertical { get; set; }
        public ProcessDto? Process { get; set; }
        public List<EquipmentDto>? Equipments { get; set; }
        public List<SeriesDto>? Series{ get; set; }
        public List<ModelDto>? Models { get; set; }
        public List<ItemMasterDto>? Items { get; set; }
    }
}
