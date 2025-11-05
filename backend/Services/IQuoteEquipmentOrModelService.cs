using Parason_Api.DTOs;

namespace Parason_Api.Services
{
    public interface IQuoteEquipmentOrModelService
    {
        Task<List<QuoteEquipmentOrModelDto>> GetByRecordIdAsync(int recordId);
        Task<QuoteEquipmentOrModelDto?> GetByIdAsync(int qeomId);
        Task<QuoteEquipmentOrModelDto> CreateAsync(CreateQuoteEquipmentOrModelDto dto);
        Task<QuoteEquipmentOrModelDto> UpdateAsync(int qeomId, CreateQuoteEquipmentOrModelDto dto);
        Task<bool> DeleteAsync(int qeomId);
    }
}
