using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parason_Api.Models
{
    [Table("ItemMaster", Schema = "dbo")]
    public class ItemMaster
    {
        [Key]
        public int ItemId { get; set; }

        [Required, MaxLength(50)]
        public string ItemCode { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string ItemName { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string UOM { get; set; } = "EA";

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        public string CreatedBy { get; set; } = "System";

        [MaxLength(100)]
        public string? Description { get; set; }

        public DateTime? ModifiedAt { get; set; }

        [MaxLength(100)]
        public string? ModifiedBy { get; set; }

        // Navigation properties
        public virtual ICollection<Price> Prices { get; set; } = new List<Price>();
        public virtual ICollection<ScopeOfSupply> ScopeOfSupplies { get; set; } = new List<ScopeOfSupply>();
    }
}
