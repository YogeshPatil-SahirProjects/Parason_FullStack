using System;
using System.Collections.Generic;

namespace Parason_Api.DTOs;

public class QuoteHeaderDto
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
    public List<QuoteVerticalDto>? QuoteVerticals { get; set; }
}

public class CreateQuoteHeaderDto
{
    public byte QuoteRevision { get; set; } = 0;
    public string QuoteNumber { get; set; } = null!;
    public string QuoteName { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public string Status { get; set; } = "Draft";
    public string Currency { get; set; } = "INR";
    public int ValidityDays { get; set; } = 30;
    public string? Notes { get; set; }
}

public class UpdateQuoteHeaderDto
{
    public byte QuoteRevision { get; set; }
    public string QuoteNumber { get; set; } = null!;
    public string QuoteName { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Currency { get; set; } = null!;
    public int ValidityDays { get; set; }
    public string? Notes { get; set; }
}
