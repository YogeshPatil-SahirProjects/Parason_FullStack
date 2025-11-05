using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Services;

public class AttributeDefService : IAttributeDefService
{
    private readonly ParasonDbContext _context;

    public AttributeDefService(ParasonDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AttributeDefDto>> GetAllAsync()
    {
        return await _context.AttributeDefs
            .Select(a => MapToDto(a))
            .ToListAsync();
    }

    public async Task<AttributeDefDto?> GetByIdAsync(int id)
    {
        var attribute = await _context.AttributeDefs.FindAsync(id);
        return attribute == null ? null : MapToDto(attribute);
    }

    public async Task<AttributeDefDto> CreateAsync(CreateAttributeDefDto dto)
    {
        var attribute = new AttributeDef
        {
            AttributeCode = dto.AttributeCode,
            AttributeName = dto.AttributeName,
            DataType = dto.DataType,
            UnitDefault = dto.UnitDefault,
            Description = dto.Description,
            IsActive = dto.IsActive
        };

        _context.AttributeDefs.Add(attribute);
        await _context.SaveChangesAsync();

        return MapToDto(attribute);
    }

    public async Task<bool> UpdateAsync(int id, UpdateAttributeDefDto dto)
    {
        var attribute = await _context.AttributeDefs.FindAsync(id);

        if (attribute == null)
            return false;

        attribute.AttributeCode = dto.AttributeCode;
        attribute.AttributeName = dto.AttributeName;
        attribute.DataType = dto.DataType;
        attribute.UnitDefault = dto.UnitDefault;
        attribute.Description = dto.Description;
        attribute.IsActive = dto.IsActive;
        attribute.ModifiedAt = DateTime.UtcNow;
        attribute.ModifiedBy = "System";

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var attribute = await _context.AttributeDefs.FindAsync(id);

        if (attribute == null)
            return false;

        _context.AttributeDefs.Remove(attribute);
        await _context.SaveChangesAsync();
        return true;
    }

    private static AttributeDefDto MapToDto(AttributeDef attribute)
    {
        return new AttributeDefDto
        {
            AttributeId = attribute.AttributeId,
            AttributeCode = attribute.AttributeCode,
            AttributeName = attribute.AttributeName,
            DataType = attribute.DataType,
            UnitDefault = attribute.UnitDefault,
            Description = attribute.Description,
            IsActive = attribute.IsActive,
            CreatedAt = attribute.CreatedAt,
            CreatedBy = attribute.CreatedBy,
            ModifiedAt = attribute.ModifiedAt,
            ModifiedBy = attribute.ModifiedBy
        };
    }
}
