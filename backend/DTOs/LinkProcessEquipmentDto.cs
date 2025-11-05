namespace Parason_Api.DTOs
{
    public class LinkProcessEquipmentDto
    {
        public int ProcessID { get; set; }
        public int EquipmentID { get; set; }
        public int? SequenceNo { get; set; }
        public bool IsRequired { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
