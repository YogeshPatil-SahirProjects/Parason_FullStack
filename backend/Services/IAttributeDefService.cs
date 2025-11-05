using Parason_Api.DTOs;

namespace Parason_Api.Services
{
    public interface IAttributeDefService
    {
        Task<PagedResponse<AttributeDefDto>> GetAllAsync(PaginationParams paginationParams);
        Task<AttributeDefDto?> GetByIdAsync(int id);
        Task<AttributeDefDto> CreateAsync(CreateAttributeDefDto dto, string createdBy);
        Task<AttributeDefDto?> UpdateAsync(int id, UpdateAttributeDefDto dto, string modifiedBy);
        Task<bool> DeleteAsync(int id);
    }
}
