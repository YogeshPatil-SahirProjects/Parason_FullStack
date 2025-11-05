using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class VerticalArea
{
    public int VerticalId { get; set; }

    public string VerticalCode { get; set; } = null!;

    public string VerticalName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<LVerticalProcess> LVerticalProcesses { get; set; } = new List<LVerticalProcess>();

    public virtual ICollection<QuoteVertical> QuoteVerticals { get; set; } = new List<QuoteVertical>();
}
