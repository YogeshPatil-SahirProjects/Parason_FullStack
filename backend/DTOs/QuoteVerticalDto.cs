namespace Parason_Api.DTOs
{
    public class QuoteVerticalDto
    {
        public int RecordID { get; set; }
        public int QuoteID { get; set; }
        public byte QuoteRevision { get; set; }
        public string Layer { get; set; } = string.Empty;
        public int VerticalID { get; set; }
        public int ProcessID { get; set; }
        public string? VerticalName { get; set; }
        public string? ProcessName { get; set; }
        public List<QuoteEquipmentOrModelDto>? EquipmentsOrModels { get; set; }
        public List<ScopeOfSupplyDto>? ScopeItems { get; set; }
        public List<SpecDetailsDto>? Specifications { get; set; }
    }
}
