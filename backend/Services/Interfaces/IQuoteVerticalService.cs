using Parason_Api.DTOs;

namespace Parason_Api.Services.Interfaces;

public interface IQuoteVerticalService
{
    Task<IEnumerable<QuoteVerticalDto>> GetAllAsync();
    Task<QuoteVerticalDto?> GetByIdAsync(int id);
    Task<QuoteVerticalDto> CreateAsync(CreateQuoteVerticalDto dto);
    Task<bool> UpdateAsync(int id, UpdateQuoteVerticalDto dto);
    Task<bool> DeleteAsync(int id);
}
