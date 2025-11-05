using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parason_Api.Models
{
    [Table("Quote_Vertical", Schema = "dbo")]
    public class QuoteVertical
    {
        [Key]
        public int RecordID { get; set; }

        public int QuoteID { get; set; }
        public byte QuoteRevision { get; set; }

        [Required, MaxLength(50)]
        public string Layer { get; set; } = string.Empty;

        public int VerticalID { get; set; }
        public int ProcessID { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        public string CreatedBy { get; set; } = "System";

        // Navigation properties
        public virtual QuoteHeader QuoteHeader { get; set; } = null!;

        [ForeignKey("VerticalID")]
        public virtual VerticalArea VerticalArea { get; set; } = null!;

        [ForeignKey("ProcessID")]
        public virtual Process Process { get; set; } = null!;

        public virtual ICollection<QuoteEquipmentOrModel> QuoteEquipmentOrModels { get; set; } = new List<QuoteEquipmentOrModel>();
        public virtual ICollection<ScopeOfSupply> ScopeOfSupplies { get; set; } = new List<ScopeOfSupply>();
        public virtual ICollection<SpecDetails> SpecDetails { get; set; } = new List<SpecDetails>();
    }
}
