using Parason_Api.DTOs;

namespace Parason_Api.Services.Interfaces;

public interface ISeriesService
{
    Task<IEnumerable<SeriesDto>> GetAllAsync();
    Task<SeriesDto?> GetByIdAsync(int id);
    Task<SeriesDto> CreateAsync(CreateSeriesDto dto);
    Task<bool> UpdateAsync(int id, UpdateSeriesDto dto);
    Task<bool> DeleteAsync(int id);
}
