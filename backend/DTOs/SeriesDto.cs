using System;

namespace Parason_Api.DTOs;

public class SeriesDto
{
    public int SeriesId { get; set; }
    public int EquipmentId { get; set; }
    public string SeriesCode { get; set; } = null!;
    public string SeriesName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }

    // Navigation property
    public string? EquipmentName { get; set; }
}

public class CreateSeriesDto
{
    public int EquipmentId { get; set; }
    public string SeriesCode { get; set; } = null!;
    public string SeriesName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}

public class UpdateSeriesDto
{
    public int EquipmentId { get; set; }
    public string SeriesCode { get; set; } = null!;
    public string SeriesName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
