using Parason_Api.DTOs;

namespace Parason_Api.Services
{
    public interface IModelService
    {
        Task<PagedResponse<ModelDto>> GetAllAsync(PaginationParams paginationParams);
        Task<ModelDto?> GetByIdAsync(int id);
        Task<ModelDto> CreateAsync(CreateModelDto dto, string createdBy);
        Task<ModelDto?> UpdateAsync(int id, UpdateModelDto dto, string modifiedBy);
        Task<bool> DeleteAsync(int id);
        Task<List<ModelDto>> GetModelsBySeriesAsync(int seriesId);
    }
}
