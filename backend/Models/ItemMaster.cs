using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class ItemMaster
{
    public int ItemId { get; set; }

    public string ItemCode { get; set; } = null!;

    public string ItemName { get; set; } = null!;

    public string Uom { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<Price> Prices { get; set; } = new List<Price>();

    public virtual ICollection<ScopeOfSupply> ScopeOfSupplies { get; set; } = new List<ScopeOfSupply>();
}
