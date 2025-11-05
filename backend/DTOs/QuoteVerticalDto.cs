using System;

namespace Parason_Api.DTOs;

public class QuoteVerticalDto
{
    public int RecordId { get; set; }
    public int QuoteId { get; set; }
    public byte QuoteRevision { get; set; }
    public string Layer { get; set; } = null!;
    public int VerticalId { get; set; }
    public int ProcessId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;

    // Navigation properties
    public string? VerticalName { get; set; }
    public string? ProcessName { get; set; }
}

public class CreateQuoteVerticalDto
{
    public int QuoteId { get; set; }
    public byte QuoteRevision { get; set; }
    public string Layer { get; set; } = null!;
    public int VerticalId { get; set; }
    public int ProcessId { get; set; }
}

public class UpdateQuoteVerticalDto
{
    public int QuoteId { get; set; }
    public byte QuoteRevision { get; set; }
    public string Layer { get; set; } = null!;
    public int VerticalId { get; set; }
    public int ProcessId { get; set; }
}
