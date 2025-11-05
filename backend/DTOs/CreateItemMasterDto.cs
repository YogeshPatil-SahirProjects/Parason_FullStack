namespace Parason_Api.DTOs
{
    public class CreateItemMasterDto
    {
        public string ItemCode { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public string UOM { get; set; } = "EA";
        public bool IsActive { get; set; } = true;
    }
}
