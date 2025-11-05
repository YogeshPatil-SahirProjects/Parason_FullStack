using System;

namespace Parason_Api.DTOs;

public class ProcessDto
{
    public int ProcessId { get; set; }
    public string ProcessCode { get; set; } = null!;
    public string ProcessName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
}

public class CreateProcessDto
{
    public string ProcessCode { get; set; } = null!;
    public string ProcessName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}

public class UpdateProcessDto
{
    public string ProcessCode { get; set; } = null!;
    public string ProcessName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
