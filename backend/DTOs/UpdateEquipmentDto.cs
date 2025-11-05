namespace Parason_Api.DTOs
{
    public class UpdateEquipmentDto
    {
        public string EquipmentCode { get; set; } = string.Empty;
        public string EquipmentName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
