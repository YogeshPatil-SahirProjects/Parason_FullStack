using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class Process
{
    public int ProcessId { get; set; }

    public string ProcessCode { get; set; } = null!;

    public string ProcessName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<LProcessEquipment> LProcessEquipments { get; set; } = new List<LProcessEquipment>();

    public virtual ICollection<LVerticalProcess> LVerticalProcesses { get; set; } = new List<LVerticalProcess>();

    public virtual ICollection<QuoteVertical> QuoteVerticals { get; set; } = new List<QuoteVertical>();
}
