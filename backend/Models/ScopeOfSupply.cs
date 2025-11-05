using System.ComponentModel.DataAnnotations.Schema;

namespace Parason_Api.Models
{
    [Table("ScopeOfSupply", Schema = "dbo")]
    public class ScopeOfSupply
    {
        public int RecordID { get; set; }
        public int? ModelID { get; set; }
        public int ItemId { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? Price_INR { get; set; }
        public bool IsMandatory { get; set; }
        public int Quantity { get; set; } = 1;

        // Navigation properties
        [ForeignKey("RecordID")]
        public virtual QuoteVertical QuoteVertical { get; set; } = null!;

        [ForeignKey("ModelID")]
        public virtual Model? Model { get; set; }

        [ForeignKey("ItemId")]
        public virtual ItemMaster Item { get; set; } = null!;        
    }
}
