using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parason_Api.Models
{
    [Table("L_Equipment_Attribute", Schema = "dbo")]
    public class EquipmentAttribute
    {
        public int EquipmentID { get; set; }
        public int AttributeID { get; set; }
        public int? SequenceNo { get; set; }

        [MaxLength(50)]
        public string? Unit { get; set; }

        public bool IsRequired { get; set; }
        public bool IsEditable { get; set; }

        [MaxLength(40)]
        public string? AttributeCategory { get; set; }

        // Navigation properties
        [ForeignKey("EquipmentID")]
        public virtual Equipment Equipment { get; set; } = null!;

        [ForeignKey("AttributeID")]
        public virtual AttributeDef AttributeDef { get; set; } = null!;
    }
}
