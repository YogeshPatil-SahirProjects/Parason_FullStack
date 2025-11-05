using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class QuoteHeader
{
    public int QuoteId { get; set; }

    public byte QuoteRevision { get; set; }

    public string QuoteNumber { get; set; } = null!;

    public string QuoteName { get; set; } = null!;

    public string CustomerName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Currency { get; set; } = null!;

    public int ValidityDays { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<QuoteVertical> QuoteVerticals { get; set; } = new List<QuoteVertical>();
}
