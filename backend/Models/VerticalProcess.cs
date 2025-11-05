using System.ComponentModel.DataAnnotations.Schema;

namespace Parason_Api.Models
{
    [Table("L_Vertical_Process", Schema = "dbo")]
    public class VerticalProcess
    {
        public int VerticalID { get; set; }
        public int ProcessID { get; set; }
        public int? SequenceNo { get; set; }
        public bool IsRequired { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        [ForeignKey("VerticalID")]
        public virtual VerticalArea VerticalArea { get; set; } = null!;

        [ForeignKey("ProcessID")]
        public virtual Process Process { get; set; } = null!;
    }
}
