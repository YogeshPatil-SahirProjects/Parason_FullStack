using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class ScopeOfSupply
{
    public int RecordId { get; set; }

    public int? ModelId { get; set; }

    public int ItemId { get; set; }

    public decimal? PriceInr { get; set; }

    public int Quantity { get; set; }

    public virtual ItemMaster Item { get; set; } = null!;

    public virtual Model? Model { get; set; }

    public virtual QuoteVertical Record { get; set; } = null!;
}
