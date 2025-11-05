using Parason_Api.DTOs;

namespace Parason_Api.Services.Interfaces;

public interface ISpecDetailService
{
    Task<IEnumerable<SpecDetailDto>> GetAllAsync();
    Task<SpecDetailDto?> GetByIdAsync(int id);
    Task<SpecDetailDto> CreateAsync(CreateSpecDetailDto dto);
    Task<bool> UpdateAsync(int id, UpdateSpecDetailDto dto);
    Task<bool> DeleteAsync(int id);
}
