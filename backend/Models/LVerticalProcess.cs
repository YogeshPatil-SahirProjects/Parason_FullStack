using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class LVerticalProcess
{
    public int VerticalId { get; set; }

    public int ProcessId { get; set; }

    public int? SequenceNo { get; set; }

    public bool IsRequired { get; set; }

    public bool IsActive { get; set; }

    public virtual Process Process { get; set; } = null!;

    public virtual VerticalArea Vertical { get; set; } = null!;
}
