using Parason_Api.DTOs;

namespace Parason_Api.Services
{
    public interface IPriceService
    {
        Task<PagedResponse<PriceDto>> GetAllAsync(PaginationParams paginationParams);
        Task<PriceDto?> GetByIdAsync(int id);
        Task<PriceDto> CreateAsync(CreatePriceDto dto, string createdBy);
        Task<PriceDto?> UpdateAsync(int id, UpdatePriceDto dto, string createdBy);
        Task<bool> DeleteAsync(int id);
        Task<decimal?> GetLatestPriceForModelAsync(int modelId);
        Task<decimal?> GetLatestPriceForEquipmentAsync(int equipmentId);
        Task<decimal?> GetLatestPriceForItemAsync(int itemId);
        Task<PriceCalculationResponseDto> CalculatePriceAsync(PriceCalculationRequestDto request);
    }
}
