using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parason_Api.Models
{
    [Table("Equipment", Schema = "dbo")]
    public class Equipment
    {
        [Key]
        public int EquipmentID { get; set; }

        [Required, MaxLength(50)]
        public string EquipmentCode { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string EquipmentName { get; set; } = string.Empty;

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
        public virtual ICollection<Series> Series { get; set; } = new List<Series>();
        public virtual ICollection<ProcessEquipment> ProcessEquipments { get; set; } = new List<ProcessEquipment>();
        public virtual ICollection<EquipmentAttribute> EquipmentAttributes { get; set; } = new List<EquipmentAttribute>();
        public virtual ICollection<EquipmentAttributeValue> EquipmentAttributeValues { get; set; } = new List<EquipmentAttributeValue>();
        public virtual ICollection<ImageRef> ImageRefs { get; set; } = new List<ImageRef>();
        public virtual ICollection<Price> Prices { get; set; } = new List<Price>();
        public virtual ICollection<QuoteEquipmentOrModel> QuoteEquipmentOrModels { get; set; } = new List<QuoteEquipmentOrModel>();
        public virtual ICollection<SpecDetails> SpecDetails { get; set; } = new List<SpecDetails>();
    }
}
