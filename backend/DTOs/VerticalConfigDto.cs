using System.Collections.Generic;

namespace Parason_Api.DTOs;

public class VerticalConfigDto
{
    public int VerticalID { get; set; }
    public string VerticalName { get; set; } = null!;
    public string Layer { get; set; } = null!;
    public decimal Total_Price { get; set; }
    public List<ProcessConfigDto> Processes { get; set; } = new();
}

public class ProcessConfigDto
{
    public int ProcessID { get; set; }
    public string ProcessCode { get; set; } = null!;
    public string ProcessName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public List<EquipmentConfigDto> Equipments { get; set; } = new();
    public List<ScopeOfSupplyConfigDto> ScopeItems { get; set; } = new();
    public List<SpecDetailConfigDto> Specifications { get; set; } = new();
}

public class EquipmentConfigDto
{
    public int EquipmentID { get; set; }
    public string EquipmentCode { get; set; } = null!;
    public string EquipmentName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public List<SeriesConfigDto> Series { get; set; } = new();
}

public class SeriesConfigDto
{
    public int SeriesID { get; set; }
    public string SeriesCode { get; set; } = null!;
    public string SeriesName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public List<ModelConfigDto> Models { get; set; } = new();
}

public class ModelConfigDto
{
    public int ModelID { get; set; }
    public int SeriesID { get; set; }
    public string ModelCode { get; set; } = null!;
    public string ModelName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public string? SeriesName { get; set; }
    public decimal? BasePrice { get; set; }
    public int? Quantity { get; set; }
}

public class ScopeOfSupplyConfigDto
{
    public int RecordID { get; set; }
    public int ModelID { get; set; }
    public int ItemId { get; set; }
    public decimal Price_INR { get; set; }
    public int Quantity { get; set; }
    public string ItemName { get; set; } = null!;
    public string ItemCode { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsMandatory { get; set; }
    public string? ModelName { get; set; }
}

public class SpecDetailConfigDto
{
    public int RecordID { get; set; }
    public int EquipmentID { get; set; }
    public int ModelID { get; set; }
    public int AttributeID { get; set; }
    public int ListValueID { get; set; }
    public decimal NumValue { get; set; }
    public string TextValue { get; set; } = null!;
    public bool BoolValue { get; set; }
    public string AttributeName { get; set; } = null!;
    public string DataType { get; set; } = null!;
    public string ListValueDisplay { get; set; } = null!;
}
