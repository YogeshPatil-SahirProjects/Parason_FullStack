using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Services;

public class PriceService : IPriceService
{
    private readonly ParasonDbContext _context;

    public PriceService(ParasonDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PriceDto>> GetAllAsync()
    {
        return await _context.Prices
            .Include(p => p.Equipment)
            .Include(p => p.Model)
            .Include(p => p.Item)
            .Select(p => MapToDto(p))
            .ToListAsync();
    }

    public async Task<PriceDto?> GetByIdAsync(int id)
    {
        var price = await _context.Prices
            .Include(p => p.Equipment)
            .Include(p => p.Model)
            .Include(p => p.Item)
            .FirstOrDefaultAsync(p => p.PriceId == id);
        return price == null ? null : MapToDto(price);
    }

    public async Task<PriceDto> CreateAsync(CreatePriceDto dto)
    {
        var price = new Price
        {
            EquipmentId = dto.EquipmentId,
            ModelId = dto.ModelId,
            ItemId = dto.ItemId,
            BasePriceInr = dto.BasePriceInr,
            EffectiveFrom = dto.EffectiveFrom ?? DateTime.UtcNow
        };

        _context.Prices.Add(price);
        await _context.SaveChangesAsync();

        var created = await _context.Prices
            .Include(p => p.Equipment)
            .Include(p => p.Model)
            .Include(p => p.Item)
            .FirstAsync(p => p.PriceId == price.PriceId);

        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, UpdatePriceDto dto)
    {
        var price = await _context.Prices.FindAsync(id);

        if (price == null)
            return false;

        price.BasePriceInr = dto.BasePriceInr;
        price.EffectiveFrom = dto.EffectiveFrom;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var price = await _context.Prices.FindAsync(id);

        if (price == null)
            return false;

        _context.Prices.Remove(price);
        await _context.SaveChangesAsync();
        return true;
    }

    private static PriceDto MapToDto(Price price)
    {
        return new PriceDto
        {
            PriceId = price.PriceId,
            EquipmentId = price.EquipmentId,
            ModelId = price.ModelId,
            ItemId = price.ItemId,
            BasePriceInr = price.BasePriceInr,
            EffectiveFrom = price.EffectiveFrom,
            CreatedAt = price.CreatedAt,
            CreatedBy = price.CreatedBy,
            EquipmentName = price.Equipment?.EquipmentName,
            ModelName = price.Model?.ModelName,
            ItemName = price.Item?.ItemName
        };
    }
}
