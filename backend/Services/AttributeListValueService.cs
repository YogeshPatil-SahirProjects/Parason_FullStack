using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services;

public class AttributeListValueService : IAttributeListValueService
{
    private readonly CPQDbContext _context;

    public AttributeListValueService(CPQDbContext context)
    {
        _context = context;
    }

    public async Task<List<AttributeListValueDto>> GetByAttributeIdAsync(int attributeId)
    {
        return await _context.AttributeListValues
            .Where(a => a.AttributeID == attributeId)
            .OrderBy(a => a.SequenceNo)
            .Select(a => new AttributeListValueDto
            {
                ListValueID = a.ListValueID,
                AttributeID = a.AttributeID,
                AttributeValue = a.AttributeValue,
                Display = a.Display,
                SequenceNo = a.SequenceNo
            })
            .ToListAsync();
    }

    public async Task<AttributeListValueDto?> GetByIdAsync(int id)
    {
        var value = await _context.AttributeListValues.FindAsync(id);
        if (value == null) return null;

        return new AttributeListValueDto
        {
            ListValueID = value.ListValueID,
            AttributeID = value.AttributeID,
            AttributeValue = value.AttributeValue,
            Display = value.Display,
            SequenceNo = value.SequenceNo
        };
    }

    public async Task<AttributeListValueDto> CreateAsync(CreateAttributeListValueDto dto)
    {
        var entity = new AttributeListValue
        {
            AttributeID = dto.AttributeID,
            AttributeValue = dto.AttributeValue,
            Display = dto.Display,
            SequenceNo = dto.SequenceNo
        };

        _context.AttributeListValues.Add(entity);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(entity.ListValueID) ?? throw new Exception("Failed to create AttributeListValue");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.AttributeListValues.FindAsync(id);
        if (entity == null) return false;

        _context.AttributeListValues.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
