using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Services;

public class SeriesService : ISeriesService
{
    private readonly ParasonDbContext _context;

    public SeriesService(ParasonDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SeriesDto>> GetAllAsync()
    {
        return await _context.Series
            .Include(s => s.Equipment)
            .Select(s => MapToDto(s))
            .ToListAsync();
    }

    public async Task<SeriesDto?> GetByIdAsync(int id)
    {
        var series = await _context.Series
            .Include(s => s.Equipment)
            .FirstOrDefaultAsync(s => s.SeriesId == id);
        return series == null ? null : MapToDto(series);
    }

    public async Task<SeriesDto> CreateAsync(CreateSeriesDto dto)
    {
        var series = new Series
        {
            EquipmentId = dto.EquipmentId,
            SeriesCode = dto.SeriesCode,
            SeriesName = dto.SeriesName,
            Description = dto.Description,
            IsActive = dto.IsActive
        };

        _context.Series.Add(series);
        await _context.SaveChangesAsync();

        var created = await _context.Series
            .Include(s => s.Equipment)
            .FirstAsync(s => s.SeriesId == series.SeriesId);

        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, UpdateSeriesDto dto)
    {
        var series = await _context.Series.FindAsync(id);

        if (series == null)
            return false;

        series.EquipmentId = dto.EquipmentId;
        series.SeriesCode = dto.SeriesCode;
        series.SeriesName = dto.SeriesName;
        series.Description = dto.Description;
        series.IsActive = dto.IsActive;
        series.ModifiedAt = DateTime.UtcNow;
        series.ModifiedBy = "System";

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var series = await _context.Series.FindAsync(id);

        if (series == null)
            return false;

        _context.Series.Remove(series);
        await _context.SaveChangesAsync();
        return true;
    }

    private static SeriesDto MapToDto(Series series)
    {
        return new SeriesDto
        {
            SeriesId = series.SeriesId,
            EquipmentId = series.EquipmentId,
            SeriesCode = series.SeriesCode,
            SeriesName = series.SeriesName,
            Description = series.Description,
            IsActive = series.IsActive,
            CreatedAt = series.CreatedAt,
            CreatedBy = series.CreatedBy,
            ModifiedAt = series.ModifiedAt,
            ModifiedBy = series.ModifiedBy,
            EquipmentName = series.Equipment?.EquipmentName
        };
    }
}
