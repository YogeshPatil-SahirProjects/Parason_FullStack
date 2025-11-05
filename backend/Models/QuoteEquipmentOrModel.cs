using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parason_Api.Models
{
    [Table("Quote_Equipment_Or_Model", Schema = "dbo")]
    public class QuoteEquipmentOrModel
    {
        [Key]
        public int QEOMId { get; set; }

        public int RecordID { get; set; }

        public int? EquipmentID { get; set; }
        public int? SeriesID { get; set; }
        public int? ModelID { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? Price_INR { get; set; }

        public int Quantity { get; set; } = 1;

        // Navigation properties
        [ForeignKey("RecordID")]
        public virtual QuoteVertical QuoteVertical { get; set; } = null!;

        [ForeignKey("EquipmentID")]
        public virtual Equipment? Equipment { get; set; }

        [ForeignKey("SeriesID")]
        public virtual Series? Series { get; set; }

        [ForeignKey("ModelID")]
        public virtual Model? Model { get; set; }
    }
}
