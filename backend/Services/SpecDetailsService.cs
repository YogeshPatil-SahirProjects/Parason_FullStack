using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services;

public class SpecDetailsService : ISpecDetailsService
{
    private readonly CPQDbContext _context;

    public SpecDetailsService(CPQDbContext context)
    {
        _context = context;
    }

    public async Task<List<SpecDetailsDto>> GetByRecordIdAsync(int recordId)
    {
        return await _context.SpecDetails
            .Where(s => s.RecordID == recordId)
            .Include(s => s.AttributeDef)
            .Include(s => s.ListValue)
            .Select(s => new SpecDetailsDto
            {
                RecordID = s.RecordID,
                EquipmentID = s.EquipmentID,
                ModelID = s.ModelID,
                AttributeID = s.AttributeID,
                ListValueID = s.ListValueID,
                NumValue = s.NumValue,
                TextValue = s.TextValue,
                BoolValue = s.BoolValue,
                AttributeName = s.AttributeDef.AttributeName,
                DataType = s.AttributeDef.DataType,
                ListValueDisplay = s.ListValue.Display
            }).ToListAsync();
    }

    public async Task<SpecDetailsDto> CreateAsync(CreateSpecDetailsDto dto)
    {
        var entity = new SpecDetails
        {
            RecordID = dto.RecordID,
            EquipmentID = dto.EquipmentID,
            ModelID = dto.ModelID,
            AttributeID = dto.AttributeID,
            ListValueID = dto.ListValueID,
            NumValue = dto.NumValue,
            TextValue = dto.TextValue,
            BoolValue = dto.BoolValue
        };

        _context.SpecDetails.Add(entity);
        await _context.SaveChangesAsync();

        var attr = await _context.AttributeDefs.FindAsync(dto.AttributeID);
        var listValue = dto.ListValueID.HasValue
            ? await _context.AttributeListValues.FindAsync(dto.ListValueID.Value)
            : null;

        return new SpecDetailsDto
        {
            RecordID = entity.RecordID,
            EquipmentID = entity.EquipmentID,
            ModelID = entity.ModelID,
            AttributeID = entity.AttributeID,
            ListValueID = entity.ListValueID,
            NumValue = entity.NumValue,
            TextValue = entity.TextValue,
            BoolValue = entity.BoolValue,
            AttributeName = attr?.AttributeName,
            DataType = attr?.DataType,
            ListValueDisplay = listValue?.Display
        };
    }

    public async Task<SpecDetailsDto?> UpdateAsync(int recordId, int attributeId, CreateSpecDetailsDto dto)
    {
        var entity = await _context.SpecDetails
            .FirstOrDefaultAsync(s => s.RecordID == recordId && s.AttributeID == attributeId);

        if (entity == null) return null;

        entity.EquipmentID = dto.EquipmentID;
        entity.ModelID = dto.ModelID;
        entity.ListValueID = dto.ListValueID;
        entity.NumValue = dto.NumValue;
        entity.TextValue = dto.TextValue;
        entity.BoolValue = dto.BoolValue;

        await _context.SaveChangesAsync();

        var attr = await _context.AttributeDefs.FindAsync(dto.AttributeID);
        var listValue = dto.ListValueID.HasValue
            ? await _context.AttributeListValues.FindAsync(dto.ListValueID.Value)
            : null;

        return new SpecDetailsDto
        {
            RecordID = entity.RecordID,
            EquipmentID = entity.EquipmentID,
            ModelID = entity.ModelID,
            AttributeID = entity.AttributeID,
            ListValueID = entity.ListValueID,
            NumValue = entity.NumValue,
            TextValue = entity.TextValue,
            BoolValue = entity.BoolValue,
            AttributeName = attr?.AttributeName,
            DataType = attr?.DataType,
            ListValueDisplay = listValue?.Display
        };
    }

    public async Task<bool> DeleteAsync(int recordId, int attributeId)
    {
        var entity = await _context.SpecDetails
            .FirstOrDefaultAsync(s => s.RecordID == recordId && s.AttributeID == attributeId);

        if (entity == null) return false;

        _context.SpecDetails.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
