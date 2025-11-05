using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class LSeriesAttribute
{
    public int SeriesId { get; set; }

    public int AttributeId { get; set; }

    public int? SequenceNo { get; set; }

    public string? Unit { get; set; }

    public bool IsRequired { get; set; }

    public bool IsEditable { get; set; }

    public string? AttributeCategory { get; set; }

    public virtual AttributeDef Attribute { get; set; } = null!;

    public virtual Series Series { get; set; } = null!;
}
