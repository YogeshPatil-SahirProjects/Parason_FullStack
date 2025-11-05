using System;
using System.Collections.Generic;

namespace Parason_Api.Models;

public partial class Equipment
{
    public int EquipmentId { get; set; }

    public string EquipmentCode { get; set; } = null!;

    public string EquipmentName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<ImageRef> ImageRefs { get; set; } = new List<ImageRef>();

    public virtual ICollection<LEquipmentAttributeValue> LEquipmentAttributeValues { get; set; } = new List<LEquipmentAttributeValue>();

    public virtual ICollection<LEquipmentAttribute> LEquipmentAttributes { get; set; } = new List<LEquipmentAttribute>();

    public virtual ICollection<LProcessEquipment> LProcessEquipments { get; set; } = new List<LProcessEquipment>();

    public virtual ICollection<Price> Prices { get; set; } = new List<Price>();

    public virtual ICollection<QuoteEquipmentOrModel> QuoteEquipmentOrModels { get; set; } = new List<QuoteEquipmentOrModel>();

    public virtual ICollection<Series> Series { get; set; } = new List<Series>();

    public virtual ICollection<SpecDetail> SpecDetails { get; set; } = new List<SpecDetail>();
}
