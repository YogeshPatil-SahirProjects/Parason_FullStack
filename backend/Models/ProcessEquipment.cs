using System.ComponentModel.DataAnnotations.Schema;

namespace Parason_Api.Models
{
    [Table("L_Process_Equipment", Schema = "dbo")]
    public class ProcessEquipment
    {
        public int ProcessID { get; set; }
        public int EquipmentID { get; set; }
        public int? SequenceNo { get; set; }
        public bool IsRequired { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        [ForeignKey("ProcessID")]
        public virtual Process Process { get; set; } = null!;

        [ForeignKey("EquipmentID")]
        public virtual Equipment Equipment { get; set; } = null!;
    }
}
