using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parason_Api.Models
{
    [Table("Series", Schema = "dbo")]
    public class Series
    {
        [Key]
        public int SeriesID { get; set; }

        public int EquipmentID { get; set; }

        [Required, MaxLength(50)]
        public string SeriesCode { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string SeriesName { get; set; } = string.Empty;

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
        [ForeignKey("EquipmentID")]
        public virtual Equipment Equipment { get; set; } = null!;

        public virtual ICollection<Model> Models { get; set; } = new List<Model>();
        public virtual ICollection<SeriesAttribute> SeriesAttributes { get; set; } = new List<SeriesAttribute>();
        public virtual ICollection<ImageRef> ImageRefs { get; set; } = new List<ImageRef>();
        public virtual ICollection<QuoteEquipmentOrModel> QuoteEquipmentOrModels { get; set; } = new List<QuoteEquipmentOrModel>();
    }

}
