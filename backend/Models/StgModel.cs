using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class StgModel
{
    public string? SeriesCode { get; set; }

    public string? ModelCode { get; set; }

    public string? ModelName { get; set; }

    public string? Description { get; set; }

    public string? IsActive { get; set; }

    public string? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }
}
