using Parason_Api.DTOs;

namespace Parason_Api.Services
{
    public interface IProcessEquipmentService
    {
        Task<List<ProcessEquipmentDto>> GetByProcessIdAsync(int processId);
        Task<List<ProcessEquipmentDto>> GetByEquipmentIdAsync(int equipmentId);
        Task<ProcessEquipmentDto> LinkAsync(LinkProcessEquipmentDto dto);
        Task<bool> UnlinkAsync(int processId, int equipmentId);
        Task<ProcessEquipmentDto?> UpdateAsync(int processId, int equipmentId, LinkProcessEquipmentDto dto);
    }
}
