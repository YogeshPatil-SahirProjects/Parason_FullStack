using Parason_Api.DTOs;

namespace Parason_Api.Services
{
    public interface IProcessService
    {
        Task<List<ProcessDto>> GetAllAsync();
        Task<ProcessDto?> GetByIdAsync(int id);
        Task<ProcessDto> CreateAsync(CreateProcessDto dto, string createdBy);
        Task<ProcessDto?> UpdateAsync(int id, UpdateProcessDto dto, string modifiedBy);
        Task<bool> DeleteAsync(int id);
        Task<List<ProcessDto>> GetProcessesByVerticalAsync(int verticalId);
        Task<List<EquipmentDto>> GetEquipmentsByProcessAsync(int processId);
    }
}
