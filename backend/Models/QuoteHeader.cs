using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parason_Api.Models
{
    [Table("QuoteHeader", Schema = "dbo")]
    public class QuoteHeader
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuoteID { get; set; }

        [Key, Column(Order = 1)]
        public byte QuoteRevision { get; set; }

        [Required, MaxLength(30)]
        public string QuoteNumber { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string QuoteName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required, MaxLength(30)]
        public string Status { get; set; } = "Draft";

        [Required, MaxLength(3)]
        public string Currency { get; set; } = "INR";

        public int ValidityDays { get; set; } = 30;

        [MaxLength(2000)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        public string CreatedBy { get; set; } = "System";

        public DateTime? ModifiedAt { get; set; }

        [MaxLength(100)]
        public string? ModifiedBy { get; set; }

        // Navigation properties
        public virtual ICollection<QuoteVertical> QuoteVerticals { get; set; } = new List<QuoteVertical>();
    }
}
