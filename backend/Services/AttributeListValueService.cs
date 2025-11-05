using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Services;

public class AttributeListValueService : IAttributeListValueService
{
    private readonly ParasonDbContext _context;

    public AttributeListValueService(ParasonDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AttributeListValueDto>> GetAllAsync()
    {
        return await _context.AttributeListValues
            .Include(a => a.Attribute)
            .Select(a => MapToDto(a))
            .ToListAsync();
    }

    public async Task<AttributeListValueDto?> GetByIdAsync(int id)
    {
        var value = await _context.AttributeListValues
            .Include(a => a.Attribute)
            .FirstOrDefaultAsync(a => a.ListValueId == id);
        return value == null ? null : MapToDto(value);
    }

    public async Task<AttributeListValueDto> CreateAsync(CreateAttributeListValueDto dto)
    {
        var value = new AttributeListValue
        {
            AttributeId = dto.AttributeId,
            AttributeValue = dto.AttributeValue,
            Display = dto.Display,
            SequenceNo = dto.SequenceNo
        };

        _context.AttributeListValues.Add(value);
        await _context.SaveChangesAsync();

        var created = await _context.AttributeListValues
            .Include(a => a.Attribute)
            .FirstAsync(a => a.ListValueId == value.ListValueId);

        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, UpdateAttributeListValueDto dto)
    {
        var value = await _context.AttributeListValues.FindAsync(id);

        if (value == null)
            return false;

        value.AttributeId = dto.AttributeId;
        value.AttributeValue = dto.AttributeValue;
        value.Display = dto.Display;
        value.SequenceNo = dto.SequenceNo;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var value = await _context.AttributeListValues.FindAsync(id);

        if (value == null)
            return false;

        _context.AttributeListValues.Remove(value);
        await _context.SaveChangesAsync();
        return true;
    }

    private static AttributeListValueDto MapToDto(AttributeListValue value)
    {
        return new AttributeListValueDto
        {
            ListValueId = value.ListValueId,
            AttributeId = value.AttributeId,
            AttributeValue = value.AttributeValue,
            Display = value.Display,
            SequenceNo = value.SequenceNo,
            AttributeName = value.Attribute?.AttributeName
        };
    }
}
