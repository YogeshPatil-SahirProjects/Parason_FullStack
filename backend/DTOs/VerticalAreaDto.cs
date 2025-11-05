using System;

namespace Parason_Api.DTOs;

public class VerticalAreaDto
{
    public int VerticalId { get; set; }
    public string VerticalCode { get; set; } = null!;
    public string VerticalName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
}

public class CreateVerticalAreaDto
{
    public string VerticalCode { get; set; } = null!;
    public string VerticalName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}

public class UpdateVerticalAreaDto
{
    public string VerticalCode { get; set; } = null!;
    public string VerticalName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
