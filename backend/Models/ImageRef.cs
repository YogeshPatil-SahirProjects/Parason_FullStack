using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class ImageRef
{
    public int ImageRefId { get; set; }

    public int? EquipmentId { get; set; }

    public int? SeriesId { get; set; }

    public int? ModelId { get; set; }

    public string ImagePurpose { get; set; } = null!;

    public string ImageFileName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual Equipment? Equipment { get; set; }

    public virtual Model? Model { get; set; }

    public virtual Series? Series { get; set; }
}
