using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Services;

public class SpecDetailService : ISpecDetailService
{
    private readonly ParasonDbContext _context;

    public SpecDetailService(ParasonDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SpecDetailDto>> GetAllAsync()
    {
        return await _context.SpecDetails
            .Include(s => s.Attribute)
            .Include(s => s.Equipment)
            .Include(s => s.Model)
            .Include(s => s.ListValue)
            .Select(s => MapToDto(s))
            .ToListAsync();
    }

    public async Task<SpecDetailDto?> GetByIdAsync(int id)
    {
        var item = await _context.SpecDetails
            .Include(s => s.Attribute)
            .Include(s => s.Equipment)
            .Include(s => s.Model)
            .Include(s => s.ListValue)
            .FirstOrDefaultAsync(s => s.RecordId == id);
        return item == null ? null : MapToDto(item);
    }

    public async Task<SpecDetailDto> CreateAsync(CreateSpecDetailDto dto)
    {
        var item = new SpecDetail
        {
            RecordId = dto.RecordId,
            EquipmentId = dto.EquipmentId,
            ModelId = dto.ModelId,
            AttributeId = dto.AttributeId,
            ListValueId = dto.ListValueId,
            NumValue = dto.NumValue,
            TextValue = dto.TextValue,
            BoolValue = dto.BoolValue
        };

        _context.SpecDetails.Add(item);
        await _context.SaveChangesAsync();

        var created = await _context.SpecDetails
            .Include(s => s.Attribute)
            .Include(s => s.Equipment)
            .Include(s => s.Model)
            .Include(s => s.ListValue)
            .FirstAsync(s => s.RecordId == item.RecordId && s.AttributeId == item.AttributeId);

        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, UpdateSpecDetailDto dto)
    {
        var item = await _context.SpecDetails
            .FirstOrDefaultAsync(s => s.RecordId == id);

        if (item == null)
            return false;

        item.EquipmentId = dto.EquipmentId;
        item.ModelId = dto.ModelId;
        item.ListValueId = dto.ListValueId;
        item.NumValue = dto.NumValue;
        item.TextValue = dto.TextValue;
        item.BoolValue = dto.BoolValue;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _context.SpecDetails
            .FirstOrDefaultAsync(s => s.RecordId == id);

        if (item == null)
            return false;

        _context.SpecDetails.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    private static SpecDetailDto MapToDto(SpecDetail item)
    {
        return new SpecDetailDto
        {
            RecordId = item.RecordId,
            EquipmentId = item.EquipmentId,
            ModelId = item.ModelId,
            AttributeId = item.AttributeId,
            ListValueId = item.ListValueId,
            NumValue = item.NumValue,
            TextValue = item.TextValue,
            BoolValue = item.BoolValue,
            AttributeName = item.Attribute?.AttributeName,
            EquipmentName = item.Equipment?.EquipmentName,
            ModelName = item.Model?.ModelName,
            ListValueDisplay = item.ListValue?.Display
        };
    }
}
