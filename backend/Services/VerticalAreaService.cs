using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Services;

public class VerticalAreaService : IVerticalAreaService
{
    private readonly ParasonDbContext _context;

    public VerticalAreaService(ParasonDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<VerticalAreaDto>> GetAllAsync()
    {
        return await _context.VerticalAreas
            .Select(v => MapToDto(v))
            .ToListAsync();
    }

    public async Task<VerticalAreaDto?> GetByIdAsync(int id)
    {
        var vertical = await _context.VerticalAreas.FindAsync(id);
        return vertical == null ? null : MapToDto(vertical);
    }

    public async Task<VerticalAreaDto> CreateAsync(CreateVerticalAreaDto dto)
    {
        var vertical = new VerticalArea
        {
            VerticalCode = dto.VerticalCode,
            VerticalName = dto.VerticalName,
            Description = dto.Description,
            IsActive = dto.IsActive
        };

        _context.VerticalAreas.Add(vertical);
        await _context.SaveChangesAsync();

        return MapToDto(vertical);
    }

    public async Task<bool> UpdateAsync(int id, UpdateVerticalAreaDto dto)
    {
        var vertical = await _context.VerticalAreas.FindAsync(id);

        if (vertical == null)
            return false;

        vertical.VerticalCode = dto.VerticalCode;
        vertical.VerticalName = dto.VerticalName;
        vertical.Description = dto.Description;
        vertical.IsActive = dto.IsActive;
        vertical.ModifiedAt = DateTime.UtcNow;
        vertical.ModifiedBy = "System";

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var vertical = await _context.VerticalAreas.FindAsync(id);

        if (vertical == null)
            return false;

        _context.VerticalAreas.Remove(vertical);
        await _context.SaveChangesAsync();
        return true;
    }

    private static VerticalAreaDto MapToDto(VerticalArea vertical)
    {
        return new VerticalAreaDto
        {
            VerticalId = vertical.VerticalId,
            VerticalCode = vertical.VerticalCode,
            VerticalName = vertical.VerticalName,
            Description = vertical.Description,
            IsActive = vertical.IsActive,
            CreatedAt = vertical.CreatedAt,
            CreatedBy = vertical.CreatedBy,
            ModifiedAt = vertical.ModifiedAt,
            ModifiedBy = vertical.ModifiedBy
        };
    }
}
