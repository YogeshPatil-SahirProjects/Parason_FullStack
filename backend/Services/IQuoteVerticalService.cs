using Parason_Api.DTOs;
using static Parason_Api.DTOs.QuoteVerticalConfigurationDto;

namespace Parason_Api.Services
{
    public interface IQuoteVerticalService
    {
        Task<List<QuoteVerticalDto>> GetByQuoteIdAsync(int quoteId, byte revision);
        Task<QuoteVerticalDto?> GetByRecordIdAsync(int recordId);
        Task<QuoteVerticalDto> CreateAsync(CreateQuoteVerticalDto dto, string createdBy);
        Task<QuoteVerticalDto> UpdateAsync(int recordId, QuoteVerticalDto dto);
        Task<bool> DeleteAsync(int recordId);
        Task<QuoteVerticalHierarchyDto?> GetVerticalsInfoAsync(int quoteId, byte quoteRevision, int verticalId);
    }
}
