using Parason_Api.DTOs;

namespace Parason_Api.Services.Interfaces;

public interface IQuoteHeaderService
{
    Task<IEnumerable<QuoteHeaderDto>> GetAllAsync();
    Task<QuoteHeaderDto?> GetByIdAsync(int quoteId, byte revision);
    Task<QuoteHeaderDto> CreateAsync(CreateQuoteHeaderDto dto);
    Task<bool> UpdateAsync(int quoteId, byte revision, UpdateQuoteHeaderDto dto);
    Task<bool> DeleteAsync(int quoteId, byte revision);
    Task<PagedResult<QuoteHeaderDto>> GetPagedAsync(QuoteSearchDto searchDto);
}
