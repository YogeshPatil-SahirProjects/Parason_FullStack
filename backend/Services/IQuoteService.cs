using Parason_Api.DTOs;

namespace Parason_Api.Services
{
    public interface IQuoteService
    {
        Task<PagedResponse<QuoteHeaderDto>> GetAllAsync(PaginationParams paginationParams);
        Task<QuoteHeaderDto?> GetByIdAsync(int quoteId, byte revision);
        Task<QuoteHeaderDto> CreateAsync(CreateQuoteHeaderDto dto, string createdBy);
        Task<QuoteHeaderDto?> UpdateAsync(int quoteId, byte revision, UpdateQuoteHeaderDto dto, string modifiedBy);
        Task<bool> DeleteAsync(int quoteId, byte revision);
        Task<QuoteHeaderDto?> CreateRevisionAsync(int quoteId, byte currentRevision, string createdBy);
        Task<List<QuoteHeaderDto>> GetQuotesByCustomerAsync(string customerName);
        Task<List<QuoteHeaderDto>> GetQuotesByStatusAsync(string status);
    }
}
