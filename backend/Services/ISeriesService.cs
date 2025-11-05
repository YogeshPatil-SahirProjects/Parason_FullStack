using Parason_Api.DTOs;

namespace Parason_Api.Services
{
    public interface ISeriesService
    {
        Task<PagedResponse<SeriesDto>> GetAllAsync(PaginationParams paginationParams);
        Task<SeriesDto?> GetByIdAsync(int id);
        Task<SeriesDto> CreateAsync(CreateSeriesDto dto, string createdBy);
        Task<SeriesDto?> UpdateAsync(int id, UpdateSeriesDto dto, string modifiedBy);
        Task<bool> DeleteAsync(int id);
        Task<List<SeriesDto>> GetSeriesByEquipmentAsync(int equipmentId);
        Task<List<ModelDto>> GetModelsBySeriesId(int seriesId);
    }
}
