using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services;

public class ItemMasterService : IItemMasterService
{
    private readonly CPQDbContext _context;

    public ItemMasterService(CPQDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResponse<ItemMasterDto>> GetAllAsync(PaginationParams paginationParams)
    {
        var query = _context.ItemMasters.AsQueryable();

        if (!string.IsNullOrEmpty(paginationParams.SearchTerm))
        {
            query = query.Where(i => i.ItemCode.Contains(paginationParams.SearchTerm) ||
                                     i.ItemName.Contains(paginationParams.SearchTerm));
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
            query = query.OrderBy(i => i.ItemName);
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .Select(i => new ItemMasterDto
            {
                ItemId = i.ItemId,
                ItemCode = i.ItemCode,
                ItemName = i.ItemName,
                UOM = i.UOM,
                IsActive = i.IsActive
            })
            .ToListAsync();

        return new PagedResponse<ItemMasterDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = paginationParams.PageNumber,
            PageSize = paginationParams.PageSize
        };
    }

    public async Task<ItemMasterDto?> GetByIdAsync(int id)
    {
        var entity = await _context.ItemMasters.FindAsync(id);
        if (entity == null) return null;

        return new ItemMasterDto
        {
            ItemId = entity.ItemId,
            ItemCode = entity.ItemCode,
            ItemName = entity.ItemName,
            UOM = entity.UOM,
            IsActive = entity.IsActive
        };
    }

    public async Task<ItemMasterDto> CreateAsync(CreateItemMasterDto dto, string createdBy)
    {
        var entity = new ItemMaster
        {
            ItemCode = dto.ItemCode,
            ItemName = dto.ItemName,
            UOM = dto.UOM,
            IsActive = dto.IsActive,
            CreatedBy = createdBy
        };

        _context.ItemMasters.Add(entity);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(entity.ItemId) ?? throw new Exception("Failed to create ItemMaster");
    }

    public async Task<ItemMasterDto?> UpdateAsync(int id, UpdateItemMasterDto dto, string modifiedBy)
    {
        var entity = await _context.ItemMasters.FindAsync(id);
        if (entity == null) return null;

        entity.ItemCode = dto.ItemCode;
        entity.ItemName = dto.ItemName;
        entity.UOM = dto.UOM;
        entity.IsActive = dto.IsActive;
        entity.ModifiedBy = modifiedBy;
        entity.ModifiedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.ItemMasters.FindAsync(id);
        if (entity == null) return false;

        _context.ItemMasters.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
