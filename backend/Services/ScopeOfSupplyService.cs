using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Services;

public class ScopeOfSupplyService : IScopeOfSupplyService
{
    private readonly ParasonDbContext _context;

    public ScopeOfSupplyService(ParasonDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ScopeOfSupplyDto>> GetAllAsync()
    {
        return await _context.ScopeOfSupplies
            .Include(s => s.Item)
            .Include(s => s.Model)
            .Select(s => MapToDto(s))
            .ToListAsync();
    }

    public async Task<ScopeOfSupplyDto?> GetByIdAsync(int id)
    {
        var item = await _context.ScopeOfSupplies
            .Include(s => s.Item)
            .Include(s => s.Model)
            .FirstOrDefaultAsync(s => s.RecordId == id && s.ItemId == id);
        return item == null ? null : MapToDto(item);
    }

    public async Task<ScopeOfSupplyDto> CreateAsync(CreateScopeOfSupplyDto dto)
    {
        var item = new ScopeOfSupply
        {
            RecordId = dto.RecordId,
            ModelId = dto.ModelId,
            ItemId = dto.ItemId,
            PriceInr = dto.PriceInr,
            Quantity = dto.Quantity
        };

        _context.ScopeOfSupplies.Add(item);
        await _context.SaveChangesAsync();

        var created = await _context.ScopeOfSupplies
            .Include(s => s.Item)
            .Include(s => s.Model)
            .FirstAsync(s => s.RecordId == item.RecordId && s.ItemId == item.ItemId);

        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, UpdateScopeOfSupplyDto dto)
    {
        var item = await _context.ScopeOfSupplies
            .FirstOrDefaultAsync(s => s.RecordId == id);

        if (item == null)
            return false;

        item.ModelId = dto.ModelId;
        item.PriceInr = dto.PriceInr;
        item.Quantity = dto.Quantity;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _context.ScopeOfSupplies
            .FirstOrDefaultAsync(s => s.RecordId == id);

        if (item == null)
            return false;

        _context.ScopeOfSupplies.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    private static ScopeOfSupplyDto MapToDto(ScopeOfSupply item)
    {
        return new ScopeOfSupplyDto
        {
            RecordId = item.RecordId,
            ModelId = item.ModelId,
            ItemId = item.ItemId,
            PriceInr = item.PriceInr,
            Quantity = item.Quantity,
            ItemName = item.Item?.ItemName,
            ModelName = item.Model?.ModelName
        };
    }
}
