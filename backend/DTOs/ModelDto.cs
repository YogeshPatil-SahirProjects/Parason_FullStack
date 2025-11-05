using System;

namespace Parason_Api.DTOs;

public class ModelDto
{
    public int ModelId { get; set; }
    public int SeriesId { get; set; }
    public string ModelCode { get; set; } = null!;
    public string ModelName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }

    // Navigation properties
    public string? SeriesName { get; set; }
    public string? EquipmentName { get; set; }
}

public class CreateModelDto
{
    public int SeriesId { get; set; }
    public string ModelCode { get; set; } = null!;
    public string ModelName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}

public class UpdateModelDto
{
    public int SeriesId { get; set; }
    public string ModelCode { get; set; } = null!;
    public string ModelName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
