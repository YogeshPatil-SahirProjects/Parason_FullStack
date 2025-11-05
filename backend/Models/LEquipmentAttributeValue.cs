using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class LEquipmentAttributeValue
{
    public int EquipmentId { get; set; }

    public int AttributeId { get; set; }

    public int SequenceNo { get; set; }

    public decimal? NumValue { get; set; }

    public string? TextValue { get; set; }

    public bool? BoolValue { get; set; }

    public int? ListValueId { get; set; }

    public bool IsDefault { get; set; }

    public virtual AttributeDef Attribute { get; set; } = null!;

    public virtual Equipment Equipment { get; set; } = null!;

    public virtual AttributeListValue? ListValue { get; set; }
}
