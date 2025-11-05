using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parason_Api.Models
{
    [Table("AttributeListValue", Schema = "dbo")]
    public class AttributeListValue
    {
        [Key]
        public int ListValueID { get; set; }

        public int AttributeID { get; set; }

        [Required, MaxLength(200)]
        public string AttributeValue { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Display { get; set; }

        public int? SequenceNo { get; set; }

        // Navigation properties
        [ForeignKey("AttributeID")]
        public virtual AttributeDef AttributeDef { get; set; } = null!;

        public virtual ICollection<EquipmentAttributeValue> EquipmentAttributeValues { get; set; } = new List<EquipmentAttributeValue>();
        public virtual ICollection<SpecDetails> SpecDetails { get; set; } = new List<SpecDetails>();
    }
}
