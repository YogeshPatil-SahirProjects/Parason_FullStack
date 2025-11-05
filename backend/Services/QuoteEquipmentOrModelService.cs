using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services;

public class QuoteEquipmentOrModelService : IQuoteEquipmentOrModelService
{
    private readonly CPQDbContext _context;

    public QuoteEquipmentOrModelService(CPQDbContext context)
    {
        _context = context;
    }

    public async Task<List<QuoteEquipmentOrModelDto>> GetByRecordIdAsync(int recordId)
    {
        return await _context.QuoteEquipmentOrModels
            .Where(q => q.RecordID == recordId)
            .Select(q => new QuoteEquipmentOrModelDto
            {
                QEOMId = q.QEOMId,
                RecordID = q.RecordID,
                EquipmentID = q.EquipmentID,
                SeriesID = q.SeriesID,
                ModelID = q.ModelID,
                Price_INR = q.Price_INR,
                Quantity = q.Quantity,
                EntityName = q.Equipment != null ? q.Equipment.EquipmentName :
                             q.Model != null ? q.Model.ModelName : null
            }).ToListAsync();
    }

    public async Task<QuoteEquipmentOrModelDto?> GetByIdAsync(int qeomId)
    {
        var q = await _context.QuoteEquipmentOrModels
            .Include(qe => qe.Equipment)
            .Include(qe => qe.Model)
            .FirstOrDefaultAsync(qe => qe.QEOMId == qeomId);

        if (q == null) return null;

        return new QuoteEquipmentOrModelDto
        {
            QEOMId = q.QEOMId,
            RecordID = q.RecordID,
            EquipmentID = q.EquipmentID,
            SeriesID = q.SeriesID,
            ModelID = q.ModelID,
            Price_INR = q.Price_INR,
            Quantity = q.Quantity,
            EntityName = q.Equipment != null ? q.Equipment.EquipmentName :
                         q.Model != null ? q.Model.ModelName : null
        };
    }

    public async Task<QuoteEquipmentOrModelDto> CreateAsync(CreateQuoteEquipmentOrModelDto dto)
    {
        var entity = new QuoteEquipmentOrModel
        {
            RecordID = dto.RecordID,
            EquipmentID = dto.EquipmentID,
            SeriesID = dto.SeriesID,
            ModelID = dto.ModelID,
            Price_INR = dto.Price_INR,
            Quantity = dto.Quantity
        };

        _context.QuoteEquipmentOrModels.Add(entity);
        await _context.SaveChangesAsync();

        var equipment = dto.EquipmentID.HasValue
            ? await _context.Equipments.FindAsync(dto.EquipmentID.Value)
            : null;
        var model = dto.ModelID.HasValue
            ? await _context.Models.FindAsync(dto.ModelID.Value)
            : null;

        return new QuoteEquipmentOrModelDto
        {
            QEOMId = entity.QEOMId,
            RecordID = entity.RecordID,
            EquipmentID = entity.EquipmentID,
            SeriesID = entity.SeriesID,
            ModelID = entity.ModelID,
            Price_INR = entity.Price_INR,
            Quantity = entity.Quantity,
            EntityName = equipment?.EquipmentName ?? model?.ModelName
        };
    }

    public async Task<bool> DeleteAsync(int qeomId)
    {
        var entity = await _context.QuoteEquipmentOrModels.FindAsync(qeomId);
        if (entity == null) return false;

        _context.QuoteEquipmentOrModels.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<QuoteEquipmentOrModelDto> UpdateAsync(int qeomId, CreateQuoteEquipmentOrModelDto dto)
    { 
        ValidateDto(dto);

        var entity = await _context.QuoteEquipmentOrModels.FindAsync(qeomId);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Quote equipment/model with QEOMId {qeomId} not found.");
        }

        entity.EquipmentID = dto.EquipmentID;
        entity.SeriesID = dto.SeriesID;
        entity.ModelID = dto.ModelID;
        entity.Price_INR = dto.Price_INR;
        entity.Quantity = dto.Quantity;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(qeomId)
            ?? throw new InvalidOperationException("Failed to retrieve updated entity");
        
    }

    private void ValidateDto(CreateQuoteEquipmentOrModelDto dto)
    {
        var count = (dto.EquipmentID.HasValue ? 1 : 0) +
                    (dto.SeriesID.HasValue ? 1 : 0) +
                    (dto.ModelID.HasValue ? 1 : 0);

        if (count != 1)
        {
            throw new InvalidOperationException("Exactly one of EquipmentID, SeriesID, or ModelID must be provided.");
        }
    }
}
