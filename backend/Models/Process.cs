using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parason_Api.Models
{
    [Table("Process", Schema = "dbo")]
    public class Process
    {
        [Key]
        public int ProcessID { get; set; }

        [Required, MaxLength(50)]
        public string ProcessCode { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string ProcessName { get; set; } = string.Empty;

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
        public virtual ICollection<ProcessEquipment> ProcessEquipments { get; set; } = new List<ProcessEquipment>();
        public virtual ICollection<QuoteVertical> QuoteVerticals { get; set; } = new List<QuoteVertical>();
    }
}
