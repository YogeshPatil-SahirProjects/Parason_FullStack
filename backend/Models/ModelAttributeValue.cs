using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parason_Api.Models
{
    [Table("L_Model_AttributeValue")]
    public class ModelAttributeValue
    {
        public int ModelID { get; set; }
        public int AttributeID { get; set; }
        public decimal? NumValue { get; set; }

        [MaxLength(1000)]
        public string? TextValue { get; set; }

        public bool? BoolValue { get; set; }
        public int? ListValueID { get; set; }
        public int? SequenceNo { get; set; }
        public bool IsDefault { get; set; }

        [ForeignKey("ModelID")]
        public virtual Model Model { get; set; }

        [ForeignKey("AttributeID")]
        public virtual AttributeDef AttributeDef { get; set; }

        [ForeignKey("ListValueID")]
        public virtual AttributeListValue? ListValue { get; set; }
    }
}
