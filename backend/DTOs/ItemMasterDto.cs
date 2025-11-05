using System;

namespace Parason_Api.DTOs;

public class ItemMasterDto
{
    public int ItemId { get; set; }
    public string ItemCode { get; set; } = null!;
    public string ItemName { get; set; } = null!;
    public string Uom { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
}

public class CreateItemMasterDto
{
    public string ItemCode { get; set; } = null!;
    public string ItemName { get; set; } = null!;
    public string Uom { get; set; } = "EA";
    public bool IsActive { get; set; } = true;
}

public class UpdateItemMasterDto
{
    public string ItemCode { get; set; } = null!;
    public string ItemName { get; set; } = null!;
    public string Uom { get; set; } = null!;
    public bool IsActive { get; set; }
}
