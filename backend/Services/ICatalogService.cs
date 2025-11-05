using Parason_Api.DTOs;

namespace Parason_Api.Services
{
    public interface ICatalogService
    {
        Task<List<CatalogVerticalDto>> GetCatalogHierarchyAsync();
        Task<CatalogVerticalDto?> GetVerticalWithProcessesAsync(int verticalId);
        Task<CatalogProcessDto?> GetProcessWithEquipmentsAsync(int processId);
    }
}
