using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parason_Api.Models
{
    [Table("L_Equipment_AttributeValue", Schema = "dbo")]
    public class EquipmentAttributeValue
    {
        public int EquipmentID { get; set; }
        public int AttributeID { get; set; }
        public int SequenceNo { get; set; } = 1;

        [Column(TypeName = "decimal(18,6)")]
        public decimal? NumValue { get; set; }

        [MaxLength(1000)]
        public string? TextValue { get; set; }

        public bool? BoolValue { get; set; }

        public int? ListValueID { get; set; }

        public bool IsDefault { get; set; }

        // Navigation properties
        [ForeignKey("EquipmentID")]
        public virtual Equipment Equipment { get; set; } = null!;

        [ForeignKey("AttributeID")]
        public virtual AttributeDef AttributeDef { get; set; } = null!;

        [ForeignKey("ListValueID")]
        public virtual AttributeListValue? ListValue { get; set; }
    }
}
