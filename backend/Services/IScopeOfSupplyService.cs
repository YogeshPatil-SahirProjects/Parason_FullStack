using Parason_Api.DTOs;

namespace Parason_Api.Services
{
    public interface IScopeOfSupplyService
    {
        Task<List<ScopeOfSupplyDto>> GetByRecordIdAsync(int recordId);
        Task<ScopeOfSupplyDto> CreateAsync(CreateScopeOfSupplyDto dto);
        Task<bool> DeleteAsync(int recordId, int itemId);
    }
}
