using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Services;

public class QuoteEquipmentOrModelService : IQuoteEquipmentOrModelService
{
    private readonly ParasonDbContext _context;

    public QuoteEquipmentOrModelService(ParasonDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<QuoteEquipmentOrModelDto>> GetAllAsync()
    {
        return await _context.QuoteEquipmentOrModels
            .Include(q => q.Equipment)
            .Include(q => q.Series)
            .Include(q => q.Model)
            .Select(q => MapToDto(q))
            .ToListAsync();
    }

    public async Task<QuoteEquipmentOrModelDto?> GetByIdAsync(int id)
    {
        var item = await _context.QuoteEquipmentOrModels
            .Include(q => q.Equipment)
            .Include(q => q.Series)
            .Include(q => q.Model)
            .FirstOrDefaultAsync(q => q.Qeomid == id);
        return item == null ? null : MapToDto(item);
    }

    public async Task<QuoteEquipmentOrModelDto> CreateAsync(CreateQuoteEquipmentOrModelDto dto)
    {
        var item = new QuoteEquipmentOrModel
        {
            RecordId = dto.RecordId,
            EquipmentId = dto.EquipmentId,
            SeriesId = dto.SeriesId,
            ModelId = dto.ModelId,
            PriceInr = dto.PriceInr,
            Quantity = dto.Quantity
        };

        _context.QuoteEquipmentOrModels.Add(item);
        await _context.SaveChangesAsync();

        var created = await _context.QuoteEquipmentOrModels
            .Include(q => q.Equipment)
            .Include(q => q.Series)
            .Include(q => q.Model)
            .FirstAsync(q => q.Qeomid == item.Qeomid);

        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, UpdateQuoteEquipmentOrModelDto dto)
    {
        var item = await _context.QuoteEquipmentOrModels.FindAsync(id);

        if (item == null)
            return false;

        item.RecordId = dto.RecordId;
        item.EquipmentId = dto.EquipmentId;
        item.SeriesId = dto.SeriesId;
        item.ModelId = dto.ModelId;
        item.PriceInr = dto.PriceInr;
        item.Quantity = dto.Quantity;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _context.QuoteEquipmentOrModels.FindAsync(id);

        if (item == null)
            return false;

        _context.QuoteEquipmentOrModels.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    private static QuoteEquipmentOrModelDto MapToDto(QuoteEquipmentOrModel item)
    {
        return new QuoteEquipmentOrModelDto
        {
            Qeomid = item.Qeomid,
            RecordId = item.RecordId,
            EquipmentId = item.EquipmentId,
            SeriesId = item.SeriesId,
            ModelId = item.ModelId,
            PriceInr = item.PriceInr,
            Quantity = item.Quantity,
            EquipmentName = item.Equipment?.EquipmentName,
            SeriesName = item.Series?.SeriesName,
            ModelName = item.Model?.ModelName
        };
    }
}
