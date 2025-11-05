using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class VwPriceLatest
{
    public int PriceId { get; set; }

    public int? EquipmentId { get; set; }

    public int? ModelId { get; set; }

    public int? ItemId { get; set; }

    public decimal BasePriceInr { get; set; }

    public DateTime EffectiveFrom { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public long? Rn { get; set; }
}
