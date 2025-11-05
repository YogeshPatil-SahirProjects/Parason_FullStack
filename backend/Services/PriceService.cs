using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services;

public class PriceService : IPriceService
{
    private readonly CPQDbContext _context;

    public PriceService(CPQDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResponse<PriceDto>> GetAllAsync(PaginationParams paginationParams)
    {
        var query = _context.Prices.AsQueryable();

        if (!string.IsNullOrEmpty(paginationParams.SearchTerm))
        {
            query = query.Where(p =>
                (p.Equipment != null && p.Equipment.EquipmentName.Contains(paginationParams.SearchTerm)) ||
                (p.Model != null && p.Model.ModelName.Contains(paginationParams.SearchTerm)) ||
                (p.Item != null && p.Item.ItemName.Contains(paginationParams.SearchTerm)));
        }

        // Sorting
        if (!string.IsNullOrEmpty(paginationParams.SortBy))
        {
            query = paginationParams.SortDescending
                ? query.OrderByDescending(e => EF.Property<object>(e, paginationParams.SortBy))
                : query.OrderBy(e => EF.Property<object>(e, paginationParams.SortBy));
        }
        else
        {
            query = query.OrderByDescending(p => p.EffectiveFrom);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .Select(p => new PriceDto
            {
                PriceID = p.PriceID,
                EquipmentID = p.EquipmentID,
                ModelID = p.ModelID,
                ItemId = p.ItemId,
                BasePriceINR = p.BasePriceINR,
                EffectiveFrom = p.EffectiveFrom,
                EntityName = p.Equipment != null ? p.Equipment.EquipmentName :
                             p.Model != null ? p.Model.ModelName :
                             p.Item != null ? p.Item.ItemName : null
            })
            .ToListAsync();

        return new PagedResponse<PriceDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = paginationParams.PageNumber,
            PageSize = paginationParams.PageSize
        };
    }

    public async Task<PriceDto?> GetByIdAsync(int id)
    {
        var entity = await _context.Prices.FindAsync(id);
        if (entity == null) return null;

        return new PriceDto
        {
            PriceID = entity.PriceID,
            EquipmentID = entity.EquipmentID,
            ModelID = entity.ModelID,
            ItemId = entity.ItemId,
            BasePriceINR = entity.BasePriceINR,
            EffectiveFrom = entity.EffectiveFrom,
            EntityName = entity.Equipment?.EquipmentName ?? entity.Model?.ModelName ?? entity.Item?.ItemName
        };
    }

    public async Task<PriceDto> CreateAsync(CreatePriceDto dto, string createdBy)
    {
        var entity = new Price
        {
            EquipmentID = dto.EquipmentID,
            ModelID = dto.ModelID,
            ItemId = dto.ItemId,
            BasePriceINR = dto.BasePriceINR,
            EffectiveFrom = dto.EffectiveFrom,
            CreatedBy = createdBy
        };

        _context.Prices.Add(entity);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(entity.PriceID) ?? throw new Exception("Failed to create Price");
    }

    public async Task<PriceDto?> UpdateAsync(int id, UpdatePriceDto dto, string createdBy)
    {
        var entity = await _context.Prices.FindAsync(id);
        if (entity == null) return null;

        entity.BasePriceINR = dto.BasePriceINR;
        entity.EffectiveFrom = dto.EffectiveFrom;
        entity.CreatedBy = createdBy;
        entity.CreatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Prices.FindAsync(id);
        if (entity == null) return false;

        _context.Prices.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<decimal?> GetLatestPriceForModelAsync(int modelId)
    {
        return await _context.Prices
            .Where(p => p.ModelID == modelId)
            .OrderByDescending(p => p.EffectiveFrom)
            .Select(p => (decimal?)p.BasePriceINR)
            .FirstOrDefaultAsync();
    }

    public async Task<decimal?> GetLatestPriceForEquipmentAsync(int equipmentId)
    {
        return await _context.Prices
            .Where(p => p.EquipmentID == equipmentId)
            .OrderByDescending(p => p.EffectiveFrom)
            .Select(p => (decimal?)p.BasePriceINR)
            .FirstOrDefaultAsync();
    }

    public async Task<decimal?> GetLatestPriceForItemAsync(int itemId)
    {
        return await _context.Prices
            .Where(p => p.ItemId == itemId)
            .OrderByDescending(p => p.EffectiveFrom)
            .Select(p => (decimal?)p.BasePriceINR)
            .FirstOrDefaultAsync();
    }

    public async Task<PriceCalculationResponseDto> CalculatePriceAsync(PriceCalculationRequestDto request)
    {
        decimal basePrice = 0;
        decimal itemsTotal = 0;

        if (request.ModelID.HasValue)
        {
            basePrice = await GetLatestPriceForModelAsync(request.ModelID.Value) ?? 0;
        }
        else if (request.EquipmentID.HasValue)
        {
            basePrice = await GetLatestPriceForEquipmentAsync(request.EquipmentID.Value) ?? 0;
        }

        if (request.ItemIds != null && request.ItemIds.Any())
        {
            foreach (var itemId in request.ItemIds)
            {
                var price = await GetLatestPriceForItemAsync(itemId) ?? 0;
                itemsTotal += price;
            }
        }

        var subtotal = basePrice + itemsTotal;
        var taxAmount = subtotal * 0.18m; // Example 18% GST
        var totalPrice = subtotal + taxAmount;

        return new PriceCalculationResponseDto
        {
            BasePrice = basePrice,
            ItemsTotal = itemsTotal,
            Subtotal = subtotal,
            TaxAmount = taxAmount,
            TotalPrice = totalPrice,
            Currency = "INR"
        };
    }
}
