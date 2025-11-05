using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class AttributeListValue
{
    public int ListValueId { get; set; }

    public int AttributeId { get; set; }

    public string AttributeValue { get; set; } = null!;

    public string? Display { get; set; }

    public int? SequenceNo { get; set; }

    public virtual AttributeDef Attribute { get; set; } = null!;

    public virtual ICollection<LEquipmentAttributeValue> LEquipmentAttributeValues { get; set; } = new List<LEquipmentAttributeValue>();

    public virtual ICollection<SpecDetail> SpecDetails { get; set; } = new List<SpecDetail>();
}
