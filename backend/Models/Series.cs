using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class Series
{
    public int SeriesId { get; set; }

    public int EquipmentId { get; set; }

    public string SeriesCode { get; set; } = null!;

    public string SeriesName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual Equipment Equipment { get; set; } = null!;

    public virtual ICollection<ImageRef> ImageRefs { get; set; } = new List<ImageRef>();

    public virtual ICollection<LSeriesAttribute> LSeriesAttributes { get; set; } = new List<LSeriesAttribute>();

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();

    public virtual ICollection<QuoteEquipmentOrModel> QuoteEquipmentOrModels { get; set; } = new List<QuoteEquipmentOrModel>();
}
