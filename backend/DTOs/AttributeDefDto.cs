using System;

namespace Parason_Api.DTOs;

public class AttributeDefDto
{
    public int AttributeId { get; set; }
    public string AttributeCode { get; set; } = null!;
    public string AttributeName { get; set; } = null!;
    public string DataType { get; set; } = null!;
    public string? UnitDefault { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
}

public class CreateAttributeDefDto
{
    public string AttributeCode { get; set; } = null!;
    public string AttributeName { get; set; } = null!;
    public string DataType { get; set; } = null!;
    public string? UnitDefault { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}

public class UpdateAttributeDefDto
{
    public string AttributeCode { get; set; } = null!;
    public string AttributeName { get; set; } = null!;
    public string DataType { get; set; } = null!;
    public string? UnitDefault { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
