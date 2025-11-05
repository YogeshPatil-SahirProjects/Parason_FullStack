using Parason_Api.DTOs;

namespace Parason_Api.Services
{
    public interface IAttributeListValueService
    {
        Task<List<AttributeListValueDto>> GetByAttributeIdAsync(int attributeId);
        Task<AttributeListValueDto?> GetByIdAsync(int id);
        Task<AttributeListValueDto> CreateAsync(CreateAttributeListValueDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
