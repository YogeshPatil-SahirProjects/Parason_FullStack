using Parason_Api.DTOs;

namespace Parason_Api.Services.Interfaces;

public interface IProcessService
{
    Task<IEnumerable<ProcessDto>> GetAllAsync();
    Task<ProcessDto?> GetByIdAsync(int id);
    Task<ProcessDto> CreateAsync(CreateProcessDto dto);
    Task<bool> UpdateAsync(int id, UpdateProcessDto dto);
    Task<bool> DeleteAsync(int id);
}
