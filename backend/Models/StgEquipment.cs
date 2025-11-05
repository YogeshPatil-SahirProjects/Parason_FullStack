using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class StgEquipment
{
    public string? EquipmentCode { get; set; }

    public string? EquipmentName { get; set; }

    public string? Description { get; set; }

    public string? IsActive { get; set; }

    public string? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }
}
