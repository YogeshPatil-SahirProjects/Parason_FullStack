using Parason_Api.DTOs;

namespace Parason_Api.Services.Interfaces;

public interface IVerticalAreaService
{
    Task<IEnumerable<VerticalAreaDto>> GetAllAsync();
    Task<VerticalAreaDto?> GetByIdAsync(int id);
    Task<VerticalAreaDto> CreateAsync(CreateVerticalAreaDto dto);
    Task<bool> UpdateAsync(int id, UpdateVerticalAreaDto dto);
    Task<bool> DeleteAsync(int id);
}
