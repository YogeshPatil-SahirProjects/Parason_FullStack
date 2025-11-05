using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Services;

public class ModelService : IModelService
{
    private readonly ParasonDbContext _context;

    public ModelService(ParasonDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ModelDto>> GetAllAsync()
    {
        return await _context.Models
            .Include(m => m.Series)
            .ThenInclude(s => s.Equipment)
            .Select(m => MapToDto(m))
            .ToListAsync();
    }

    public async Task<ModelDto?> GetByIdAsync(int id)
    {
        var model = await _context.Models
            .Include(m => m.Series)
            .ThenInclude(s => s.Equipment)
            .FirstOrDefaultAsync(m => m.ModelId == id);
        return model == null ? null : MapToDto(model);
    }

    public async Task<ModelDto> CreateAsync(CreateModelDto dto)
    {
        var model = new Model
        {
            SeriesId = dto.SeriesId,
            ModelCode = dto.ModelCode,
            ModelName = dto.ModelName,
            Description = dto.Description,
            IsActive = dto.IsActive
        };

        _context.Models.Add(model);
        await _context.SaveChangesAsync();

        var created = await _context.Models
            .Include(m => m.Series)
            .ThenInclude(s => s.Equipment)
            .FirstAsync(m => m.ModelId == model.ModelId);

        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, UpdateModelDto dto)
    {
        var model = await _context.Models.FindAsync(id);

        if (model == null)
            return false;

        model.SeriesId = dto.SeriesId;
        model.ModelCode = dto.ModelCode;
        model.ModelName = dto.ModelName;
        model.Description = dto.Description;
        model.IsActive = dto.IsActive;
        model.ModifiedAt = DateTime.UtcNow;
        model.ModifiedBy = "System";

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var model = await _context.Models.FindAsync(id);

        if (model == null)
            return false;

        _context.Models.Remove(model);
        await _context.SaveChangesAsync();
        return true;
    }

    private static ModelDto MapToDto(Model model)
    {
        return new ModelDto
        {
            ModelId = model.ModelId,
            SeriesId = model.SeriesId,
            ModelCode = model.ModelCode,
            ModelName = model.ModelName,
            Description = model.Description,
            IsActive = model.IsActive,
            CreatedAt = model.CreatedAt,
            CreatedBy = model.CreatedBy,
            ModifiedAt = model.ModifiedAt,
            ModifiedBy = model.ModifiedBy,
            SeriesName = model.Series?.SeriesName,
            EquipmentName = model.Series?.Equipment?.EquipmentName
        };
    }
}
