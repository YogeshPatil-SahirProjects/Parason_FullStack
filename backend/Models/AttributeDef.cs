using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parason_Api.Models
{
    [Table("AttributeDef", Schema = "dbo")]
    public class AttributeDef
    {
        [Key]
        public int AttributeID { get; set; }

        [Required, MaxLength(50)]
        public string AttributeCode { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string AttributeName { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string DataType { get; set; } = string.Empty; // number, text, bool, list

        [MaxLength(50)]
        public string? UnitDefault { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        public string CreatedBy { get; set; } = "System";

        public DateTime? ModifiedAt { get; set; }

        [MaxLength(100)]
        public string? ModifiedBy { get; set; }

        // Navigation properties
        public virtual ICollection<AttributeListValue> ListValues { get; set; } = new List<AttributeListValue>();
        public virtual ICollection<EquipmentAttribute> EquipmentAttributes { get; set; } = new List<EquipmentAttribute>();
        public virtual ICollection<EquipmentAttributeValue> EquipmentAttributeValues { get; set; } = new List<EquipmentAttributeValue>();
        public virtual ICollection<SeriesAttribute> SeriesAttributes { get; set; } = new List<SeriesAttribute>();
        public virtual ICollection<SpecDetails> SpecDetails { get; set; } = new List<SpecDetails>();
    }
}
