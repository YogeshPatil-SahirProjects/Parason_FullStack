using Parason_Api.DTOs;

namespace Parason_Api.Services
{
    public interface IEquipmentService
    {
        Task<PagedResponse<EquipmentDto>> GetAllAsync(PaginationParams paginationParams);
        Task<EquipmentDto?> GetByIdAsync(int id);
        Task<EquipmentDto> CreateAsync(CreateEquipmentDto dto, string createdBy);
        Task<EquipmentDto?> UpdateAsync(int id, UpdateEquipmentDto dto, string modifiedBy);
        Task<bool> DeleteAsync(int id);
        Task<List<EquipmentDto>> GetEquipmentsByProcessAsync(int processId);
        Task<List<SeriesDto>> GetSeriesByEquipmentId(int equipmentId);
        Task<List<EquipmentAttributeDto>> GetEquipmentAttributes(int equipmentId);
    }
}
