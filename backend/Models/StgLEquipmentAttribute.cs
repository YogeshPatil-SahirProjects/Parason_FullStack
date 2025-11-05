using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class StgLEquipmentAttribute
{
    public string? EquipmentCode { get; set; }

    public string? AttributeCode { get; set; }

    public string? SequenceNo { get; set; }

    public string? Unit { get; set; }

    public string? IsRequired { get; set; }

    public string? IsEditable { get; set; }

    public string? AttributeCategory { get; set; }
}
