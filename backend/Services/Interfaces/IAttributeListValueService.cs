using Parason_Api.DTOs;

namespace Parason_Api.Services.Interfaces;

public interface IAttributeListValueService
{
    Task<IEnumerable<AttributeListValueDto>> GetAllAsync();
    Task<AttributeListValueDto?> GetByIdAsync(int id);
    Task<AttributeListValueDto> CreateAsync(CreateAttributeListValueDto dto);
    Task<bool> UpdateAsync(int id, UpdateAttributeListValueDto dto);
    Task<bool> DeleteAsync(int id);
}
