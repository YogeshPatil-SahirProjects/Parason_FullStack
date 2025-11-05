using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Services;

public class ItemMasterService : IItemMasterService
{
    private readonly ParasonDbContext _context;

    public ItemMasterService(ParasonDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ItemMasterDto>> GetAllAsync()
    {
        return await _context.ItemMasters
            .Select(i => MapToDto(i))
            .ToListAsync();
    }

    public async Task<ItemMasterDto?> GetByIdAsync(int id)
    {
        var item = await _context.ItemMasters.FindAsync(id);
        return item == null ? null : MapToDto(item);
    }

    public async Task<ItemMasterDto> CreateAsync(CreateItemMasterDto dto)
    {
        var item = new ItemMaster
        {
            ItemCode = dto.ItemCode,
            ItemName = dto.ItemName,
            Uom = dto.Uom,
            IsActive = dto.IsActive
        };

        _context.ItemMasters.Add(item);
        await _context.SaveChangesAsync();

        return MapToDto(item);
    }

    public async Task<bool> UpdateAsync(int id, UpdateItemMasterDto dto)
    {
        var item = await _context.ItemMasters.FindAsync(id);

        if (item == null)
            return false;

        item.ItemCode = dto.ItemCode;
        item.ItemName = dto.ItemName;
        item.Uom = dto.Uom;
        item.IsActive = dto.IsActive;
        item.ModifiedAt = DateTime.UtcNow;
        item.ModifiedBy = "System";

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _context.ItemMasters.FindAsync(id);

        if (item == null)
            return false;

        _context.ItemMasters.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    private static ItemMasterDto MapToDto(ItemMaster item)
    {
        return new ItemMasterDto
        {
            ItemId = item.ItemId,
            ItemCode = item.ItemCode,
            ItemName = item.ItemName,
            Uom = item.Uom,
            IsActive = item.IsActive,
            CreatedAt = item.CreatedAt,
            CreatedBy = item.CreatedBy,
            ModifiedAt = item.ModifiedAt,
            ModifiedBy = item.ModifiedBy
        };
    }
}
