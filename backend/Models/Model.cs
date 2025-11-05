using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class Model
{
    public int ModelId { get; set; }

    public int SeriesId { get; set; }

    public string ModelCode { get; set; } = null!;

    public string ModelName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<ImageRef> ImageRefs { get; set; } = new List<ImageRef>();

    public virtual ICollection<Price> Prices { get; set; } = new List<Price>();

    public virtual ICollection<QuoteEquipmentOrModel> QuoteEquipmentOrModels { get; set; } = new List<QuoteEquipmentOrModel>();

    public virtual ICollection<ScopeOfSupply> ScopeOfSupplies { get; set; } = new List<ScopeOfSupply>();

    public virtual Series Series { get; set; } = null!;

    public virtual ICollection<SpecDetail> SpecDetails { get; set; } = new List<SpecDetail>();
}
