using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class StgLEquipmentAttributeValue
{
    public string? EquipmentCode { get; set; }

    public string? AttributeCode { get; set; }

    public string? SequenceNo { get; set; }

    public string? NumValue { get; set; }

    public string? TextValue { get; set; }

    public string? BoolValue { get; set; }

    public string? ListValue { get; set; }

    public string? IsDefault { get; set; }
}
