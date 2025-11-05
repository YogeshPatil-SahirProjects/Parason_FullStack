using Parason_Api.DTOs;

namespace Parason_Api.Services
{
    public interface IItemMasterService
    {
        Task<PagedResponse<ItemMasterDto>> GetAllAsync(PaginationParams paginationParams);
        Task<ItemMasterDto?> GetByIdAsync(int id);
        Task<ItemMasterDto> CreateAsync(CreateItemMasterDto dto, string createdBy);
        Task<ItemMasterDto?> UpdateAsync(int id, UpdateItemMasterDto dto, string modifiedBy);
        Task<bool> DeleteAsync(int id);
    }
}
