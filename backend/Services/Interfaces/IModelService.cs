using Parason_Api.DTOs;

namespace Parason_Api.Services.Interfaces;

public interface IModelService
{
    Task<IEnumerable<ModelDto>> GetAllAsync();
    Task<ModelDto?> GetByIdAsync(int id);
    Task<ModelDto> CreateAsync(CreateModelDto dto);
    Task<bool> UpdateAsync(int id, UpdateModelDto dto);
    Task<bool> DeleteAsync(int id);
}
