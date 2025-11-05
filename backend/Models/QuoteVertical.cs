using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class QuoteVertical
{
    public int RecordId { get; set; }

    public int QuoteId { get; set; }

    public byte QuoteRevision { get; set; }

    public string Layer { get; set; } = null!;

    public int VerticalId { get; set; }

    public int ProcessId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual Process Process { get; set; } = null!;

    public virtual ICollection<QuoteEquipmentOrModel> QuoteEquipmentOrModels { get; set; } = new List<QuoteEquipmentOrModel>();

    public virtual QuoteHeader QuoteHeader { get; set; } = null!;

    public virtual ICollection<ScopeOfSupply> ScopeOfSupplies { get; set; } = new List<ScopeOfSupply>();

    public virtual ICollection<SpecDetail> SpecDetails { get; set; } = new List<SpecDetail>();

    public virtual VerticalArea Vertical { get; set; } = null!;
}
