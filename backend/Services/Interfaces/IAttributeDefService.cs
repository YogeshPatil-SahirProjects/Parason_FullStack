using Parason_Api.DTOs;

namespace Parason_Api.Services.Interfaces;

public interface IAttributeDefService
{
    Task<IEnumerable<AttributeDefDto>> GetAllAsync();
    Task<AttributeDefDto?> GetByIdAsync(int id);
    Task<AttributeDefDto> CreateAsync(CreateAttributeDefDto dto);
    Task<bool> UpdateAsync(int id, UpdateAttributeDefDto dto);
    Task<bool> DeleteAsync(int id);
}
