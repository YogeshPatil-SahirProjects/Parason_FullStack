using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parason_Api.Models
{
    [Table("ImageRef", Schema = "dbo")]
    public class ImageRef
    {
        [Key]
        public int ImageRefID { get; set; }

        public int? EquipmentID { get; set; }
        public int? SeriesID { get; set; }
        public int? ModelID { get; set; }

        [Required, MaxLength(50)]
        public string ImagePurpose { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        public string ImageFileName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        public string CreatedBy { get; set; } = "System";

        // Navigation properties
        [ForeignKey("EquipmentID")]
        public virtual Equipment? Equipment { get; set; }

        [ForeignKey("SeriesID")]
        public virtual Series? Series { get; set; }

        [ForeignKey("ModelID")]
        public virtual Model? Model { get; set; }
    }
}
