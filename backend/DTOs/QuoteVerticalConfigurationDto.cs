namespace Parason_Api.DTOs
{
    public class QuoteVerticalConfigurationDto
    {
        public class QuoteVerticalHierarchyDto
        {
            public int VerticalID { get; set; }
            public string? VerticalName { get; set; }
            public string? Layer { get; set; }
            public decimal? Total_Price { get; set; }

            // New: All processes under this vertical
            public List<ProcessHierarchyDto>? Processes { get; set; }
        }

        public class ProcessHierarchyDto : ProcessDto
        {
            // Each process can have multiple equipments/models
            public List<EquipmentHierarchyDto>? Equipments { get; set; }

            // Each process can also have scope items or specs if needed
            public List<ScopeOfSupplyDto>? ScopeItems { get; set; }
            public List<SpecDetailsDto>? Specifications { get; set; }
        }

        public class EquipmentHierarchyDto : EquipmentDto
        {
            public List<PriceQuantityDto> EquipmentOnlyItems { get; set; } = new();
            public List<SeriesHierarchyDto>? Series { get; set; }
        }

        public class PriceQuantityDto
        {
            public int QEOMId { get; set; }
            public decimal? Price_INR { get; set; }
            public int Quantity { get; set; }
        }

        public class SeriesHierarchyDto : SeriesDto
        {
            public List<ModelDto>? Models { get; set; }
        }
    }

}
