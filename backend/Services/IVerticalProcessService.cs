using Parason_Api.DTOs;

namespace Parason_Api.Services
{
    public interface IVerticalProcessService
    {
        Task<List<VerticalProcessDto>> GetByVerticalIdAsync(int verticalId);
        Task<List<VerticalProcessDto>> GetByProcessIdAsync(int processId);
        Task<VerticalProcessDto> LinkAsync(LinkVerticalProcessDto dto);
        Task<bool> UnlinkAsync(int verticalId, int processId);
        Task<VerticalProcessDto?> UpdateAsync(int verticalId, int processId, LinkVerticalProcessDto dto);
    }
}
