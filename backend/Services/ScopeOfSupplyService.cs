using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services;

public class ScopeOfSupplyService : IScopeOfSupplyService
{
    private readonly CPQDbContext _context;

    public ScopeOfSupplyService(CPQDbContext context)
    {
        _context = context;
    }

    public async Task<List<ScopeOfSupplyDto>> GetByRecordIdAsync(int recordId)
    {
        return await _context.ScopeOfSupplies
            .Where(s => s.RecordID == recordId)
            .Include(s => s.Item)
            .Select(s => new ScopeOfSupplyDto
            {
                RecordID = s.RecordID,
                ModelID = s.ModelID,
                ItemId = s.ItemId,
                Price_INR = s.Price_INR,
                Quantity = s.Quantity,
                ItemName = s.Item.ItemName,
                ItemCode = s.Item.ItemCode
            }).ToListAsync();
    }

    public async Task<ScopeOfSupplyDto> CreateAsync(CreateScopeOfSupplyDto dto)
    {
        var entity = new ScopeOfSupply
        {
            RecordID = dto.RecordID,
            ModelID = dto.ModelID,
            ItemId = dto.ItemId,
            Price_INR = dto.Price_INR,
            Quantity = dto.Quantity
        };

        _context.ScopeOfSupplies.Add(entity);
        await _context.SaveChangesAsync();

        var item = await _context.ItemMasters.FindAsync(dto.ItemId);

        return new ScopeOfSupplyDto
        {
            RecordID = entity.RecordID,
            ModelID = entity.ModelID,
            ItemId = entity.ItemId,
            Price_INR = entity.Price_INR,
            Quantity = entity.Quantity,
            ItemName = item?.ItemName,
            ItemCode = item?.ItemCode
        };
    }

    public async Task<bool> DeleteAsync(int recordId, int itemId)
    {
        var entity = await _context.ScopeOfSupplies
            .FirstOrDefaultAsync(s => s.RecordID == recordId && s.ItemId == itemId);

        if (entity == null) return false;

        _context.ScopeOfSupplies.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
