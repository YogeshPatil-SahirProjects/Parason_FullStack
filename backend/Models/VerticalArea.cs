using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parason_Api.Models
{
    [Table("Vertical_Area", Schema = "dbo")]
    public class VerticalArea
    {
        [Key]
        public int VerticalID { get; set; }

        [Required, MaxLength(50)]
        public string VerticalCode { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string VerticalName { get; set; } = string.Empty;

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
        public virtual ICollection<VerticalProcess> VerticalProcesses { get; set; } = new List<VerticalProcess>();
        public virtual ICollection<QuoteVertical> QuoteVerticals { get; set; } = new List<QuoteVertical>();
    }
}
