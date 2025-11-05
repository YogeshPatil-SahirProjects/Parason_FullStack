using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class LProcessEquipment
{
    public int ProcessId { get; set; }

    public int EquipmentId { get; set; }

    public int? SequenceNo { get; set; }

    public bool IsRequired { get; set; }

    public bool IsActive { get; set; }

    public virtual Equipment Equipment { get; set; } = null!;

    public virtual Process Process { get; set; } = null!;
}
