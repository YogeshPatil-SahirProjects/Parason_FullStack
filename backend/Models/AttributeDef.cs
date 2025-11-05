using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class AttributeDef
{
    public int AttributeId { get; set; }

    public string AttributeCode { get; set; } = null!;

    public string AttributeName { get; set; } = null!;

    public string DataType { get; set; } = null!;

    public string? UnitDefault { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<AttributeListValue> AttributeListValues { get; set; } = new List<AttributeListValue>();

    public virtual ICollection<LEquipmentAttributeValue> LEquipmentAttributeValues { get; set; } = new List<LEquipmentAttributeValue>();

    public virtual ICollection<LEquipmentAttribute> LEquipmentAttributes { get; set; } = new List<LEquipmentAttribute>();

    public virtual ICollection<LSeriesAttribute> LSeriesAttributes { get; set; } = new List<LSeriesAttribute>();

    public virtual ICollection<SpecDetail> SpecDetails { get; set; } = new List<SpecDetail>();
}
