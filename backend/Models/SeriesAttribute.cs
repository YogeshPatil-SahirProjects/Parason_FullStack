using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parason_Api.Models
{
    [Table("L_Series_Attribute", Schema = "dbo")]
    public class SeriesAttribute
    {
        public int SeriesID { get; set; }
        public int AttributeID { get; set; }
        public int? SequenceNo { get; set; }

        [MaxLength(50)]
        public string? Unit { get; set; }

        public bool IsRequired { get; set; }
        public bool IsEditable { get; set; }

        [MaxLength(40)]
        public string? AttributeCategory { get; set; }

        // Navigation properties
        [ForeignKey("SeriesID")]
        public virtual Series Series { get; set; } = null!;

        [ForeignKey("AttributeID")]
        public virtual AttributeDef AttributeDef { get; set; } = null!;
    }
}
