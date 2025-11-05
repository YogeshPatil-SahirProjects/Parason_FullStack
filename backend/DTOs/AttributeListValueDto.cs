namespace Parason_Api.DTOs;

public class AttributeListValueDto
{
    public int ListValueId { get; set; }
    public int AttributeId { get; set; }
    public string AttributeValue { get; set; } = null!;
    public string? Display { get; set; }
    public int? SequenceNo { get; set; }

    // Navigation property
    public string? AttributeName { get; set; }
}

public class CreateAttributeListValueDto
{
    public int AttributeId { get; set; }
    public string AttributeValue { get; set; } = null!;
    public string? Display { get; set; }
    public int? SequenceNo { get; set; }
}

public class UpdateAttributeListValueDto
{
    public int AttributeId { get; set; }
    public string AttributeValue { get; set; } = null!;
    public string? Display { get; set; }
    public int? SequenceNo { get; set; }
}
