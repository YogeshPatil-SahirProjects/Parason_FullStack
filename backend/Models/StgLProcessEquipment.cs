using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class StgLProcessEquipment
{
    public string? ProcessCode { get; set; }

    public string? EquipmentCode { get; set; }

    public string? SequenceNo { get; set; }

    public string? IsRequired { get; set; }

    public string? IsActive { get; set; }
}
