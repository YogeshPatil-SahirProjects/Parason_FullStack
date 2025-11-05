using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class StgSeries
{
    public string? EquipmentCode { get; set; }

    public string? SeriesCode { get; set; }

    public string? SeriesName { get; set; }

    public string? Description { get; set; }

    public string? IsActive { get; set; }

    public string? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }
}
