using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parason_Api.Models
{
    [Table("Spec_Details", Schema = "dbo")]
    public class SpecDetails
    {
        public int RecordID { get; set; }
        public int? EquipmentID { get; set; }
        public int? ModelID { get; set; }
        public int AttributeID { get; set; }

        public int? ListValueID { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal? NumValue { get; set; }

        [MaxLength(1000)]
        public string? TextValue { get; set; }

        public bool? BoolValue { get; set; }

        // Navigation properties
        [ForeignKey("RecordID")]
        public virtual QuoteVertical QuoteVertical { get; set; } = null!;

        [ForeignKey("EquipmentID")]
        public virtual Equipment? Equipment { get; set; }

        [ForeignKey("ModelID")]
        public virtual Model? Model { get; set; }

        [ForeignKey("AttributeID")]
        public virtual AttributeDef AttributeDef { get; set; } = null!;

        [ForeignKey("ListValueID")]
        public virtual AttributeListValue? ListValue { get; set; }
    }
}
