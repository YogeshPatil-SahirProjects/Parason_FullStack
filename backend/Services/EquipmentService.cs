using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Services;

public class EquipmentService : IEquipmentService
{
    private readonly ParasonDbContext _context;

    public EquipmentService(ParasonDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EquipmentDto>> GetAllAsync()
    {
        return await _context.Equipment
            .Select(e => MapToDto(e))
            .ToListAsync();
    }

    public async Task<EquipmentDto?> GetByIdAsync(int id)
    {
        var equipment = await _context.Equipment.FindAsync(id);
        return equipment == null ? null : MapToDto(equipment);
    }

    public async Task<EquipmentDto> CreateAsync(CreateEquipmentDto dto)
    {
        var equipment = new Equipment
        {
            EquipmentCode = dto.EquipmentCode,
            EquipmentName = dto.EquipmentName,
            Description = dto.Description,
            IsActive = dto.IsActive
        };

        _context.Equipment.Add(equipment);
        await _context.SaveChangesAsync();

        return MapToDto(equipment);
    }

    public async Task<bool> UpdateAsync(int id, UpdateEquipmentDto dto)
    {
        var equipment = await _context.Equipment.FindAsync(id);

        if (equipment == null)
            return false;

        equipment.EquipmentCode = dto.EquipmentCode;
        equipment.EquipmentName = dto.EquipmentName;
        equipment.Description = dto.Description;
        equipment.IsActive = dto.IsActive;
        equipment.ModifiedAt = DateTime.UtcNow;
        equipment.ModifiedBy = "System"; // TODO: Get from authenticated user

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var equipment = await _context.Equipment.FindAsync(id);

        if (equipment == null)
            return false;

        _context.Equipment.Remove(equipment);
        await _context.SaveChangesAsync();
        return true;
    }

    private static EquipmentDto MapToDto(Equipment equipment)
    {
        return new EquipmentDto
        {
            EquipmentId = equipment.EquipmentId,
            EquipmentCode = equipment.EquipmentCode,
            EquipmentName = equipment.EquipmentName,
            Description = equipment.Description,
            IsActive = equipment.IsActive,
            CreatedAt = equipment.CreatedAt,
            CreatedBy = equipment.CreatedBy,
            ModifiedAt = equipment.ModifiedAt,
            ModifiedBy = equipment.ModifiedBy
        };
    }
}
