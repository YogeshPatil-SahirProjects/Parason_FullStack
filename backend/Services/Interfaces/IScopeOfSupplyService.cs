using Parason_Api.DTOs;

namespace Parason_Api.Services.Interfaces;

public interface IScopeOfSupplyService
{
    Task<IEnumerable<ScopeOfSupplyDto>> GetAllAsync();
    Task<ScopeOfSupplyDto?> GetByIdAsync(int id);
    Task<ScopeOfSupplyDto> CreateAsync(CreateScopeOfSupplyDto dto);
    Task<bool> UpdateAsync(int id, UpdateScopeOfSupplyDto dto);
    Task<bool> DeleteAsync(int id);
}
