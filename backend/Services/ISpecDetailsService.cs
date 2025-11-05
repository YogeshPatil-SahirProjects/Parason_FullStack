using Parason_Api.DTOs;

namespace Parason_Api.Services
{
    public interface ISpecDetailsService
    {
        Task<List<SpecDetailsDto>> GetByRecordIdAsync(int recordId);
        Task<SpecDetailsDto> CreateAsync(CreateSpecDetailsDto dto);
        Task<SpecDetailsDto?> UpdateAsync(int recordId, int attributeId, CreateSpecDetailsDto dto);
        Task<bool> DeleteAsync(int recordId, int attributeId);
    }
}
