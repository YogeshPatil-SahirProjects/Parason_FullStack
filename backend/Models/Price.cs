using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parason_Api.Models
{
    [Table("Price", Schema = "dbo")]
    public class Price
    {
        [Key]
        public int PriceID { get; set; }

        public int? EquipmentID { get; set; }
        public int? ModelID { get; set; }
        public int? ItemId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BasePriceINR { get; set; }

        public DateTime EffectiveFrom { get; set; } = DateTime.UtcNow;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        public string CreatedBy { get; set; } = "System";

        // Navigation properties
        [ForeignKey("EquipmentID")]
        public virtual Equipment? Equipment { get; set; }

        [ForeignKey("ModelID")]
        public virtual Model? Model { get; set; }

        [ForeignKey("ItemId")]
        public virtual ItemMaster? Item { get; set; }
    }
}
