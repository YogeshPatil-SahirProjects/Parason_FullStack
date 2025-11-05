using Parason_Api.DTOs;

namespace Parason_Api.Services.Interfaces;

public interface IItemMasterService
{
    Task<IEnumerable<ItemMasterDto>> GetAllAsync();
    Task<ItemMasterDto?> GetByIdAsync(int id);
    Task<ItemMasterDto> CreateAsync(CreateItemMasterDto dto);
    Task<bool> UpdateAsync(int id, UpdateItemMasterDto dto);
    Task<bool> DeleteAsync(int id);
}
