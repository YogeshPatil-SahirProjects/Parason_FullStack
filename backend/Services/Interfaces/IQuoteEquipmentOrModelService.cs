using Parason_Api.DTOs;

namespace Parason_Api.Services.Interfaces;

public interface IQuoteEquipmentOrModelService
{
    Task<IEnumerable<QuoteEquipmentOrModelDto>> GetAllAsync();
    Task<QuoteEquipmentOrModelDto?> GetByIdAsync(int id);
    Task<QuoteEquipmentOrModelDto> CreateAsync(CreateQuoteEquipmentOrModelDto dto);
    Task<bool> UpdateAsync(int id, UpdateQuoteEquipmentOrModelDto dto);
    Task<bool> DeleteAsync(int id);
}
