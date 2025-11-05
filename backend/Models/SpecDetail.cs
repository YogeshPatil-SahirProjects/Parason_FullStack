using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class SpecDetail
{
    public int RecordId { get; set; }

    public int? EquipmentId { get; set; }

    public int? ModelId { get; set; }

    public int AttributeId { get; set; }

    public int? ListValueId { get; set; }

    public decimal? NumValue { get; set; }

    public string? TextValue { get; set; }

    public bool? BoolValue { get; set; }

    public virtual AttributeDef Attribute { get; set; } = null!;

    public virtual Equipment? Equipment { get; set; }

    public virtual AttributeListValue? ListValue { get; set; }

    public virtual Model? Model { get; set; }

    public virtual QuoteVertical Record { get; set; } = null!;
}
