using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class StgAttributeDef
{
    public string? AttributeCode { get; set; }

    public string? AttributeName { get; set; }

    public string? DataType { get; set; }

    public string? UnitDefault { get; set; }

    public string? Description { get; set; }

    public string? IsActive { get; set; }

    public string? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }
}
