using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parason_Api.Models
{
    [Table("Model", Schema = "dbo")]
    public class Model
    {
        [Key]
        public int ModelID { get; set; }

        public int SeriesID { get; set; }

        [Required, MaxLength(50)]
        public string ModelCode { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string ModelName { get; set; } = string.Empty;

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
        [ForeignKey("SeriesID")]
        public virtual Series Series { get; set; } = null!;

        public virtual ICollection<ImageRef> ImageRefs { get; set; } = new List<ImageRef>();
        public virtual ICollection<Price> Prices { get; set; } = new List<Price>();
        public virtual ICollection<QuoteEquipmentOrModel> QuoteEquipmentOrModels { get; set; } = new List<QuoteEquipmentOrModel>();
        public virtual ICollection<ScopeOfSupply> ScopeOfSupplies { get; set; } = new List<ScopeOfSupply>();
        public virtual ICollection<SpecDetails> SpecDetails { get; set; } = new List<SpecDetails>();
    }
}
