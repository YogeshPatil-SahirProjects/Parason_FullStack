using Parason_Api.DTOs;

namespace Parason_Api.Services
{
    public interface IVerticalAreaService
    {
        Task<List<VerticalAreaDto>> GetAllAsync();
        Task<VerticalAreaDto?> GetByIdAsync(int id);
        Task<VerticalAreaDto> CreateAsync(CreateVerticalAreaDto dto, string createdBy);
        Task<VerticalAreaDto?> UpdateAsync(int id, UpdateVerticalAreaDto dto, string modifiedBy);
        Task<bool> DeleteAsync(int id);
        Task<List<CatalogVerticalDto>> GetVerticalCatalogAsync();
        Task<List<ProcessDto>> GetProcessForSelectedVerticalAsync(int id);
    }
}
