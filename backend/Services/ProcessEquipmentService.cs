using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services;

public class ProcessEquipmentService : IProcessEquipmentService
{
    private readonly CPQDbContext _context;

    public ProcessEquipmentService(CPQDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProcessEquipmentDto>> GetByProcessIdAsync(int processId)
    {
        return await _context.ProcessEquipments
            .Where(pe => pe.ProcessID == processId)
            .Include(pe => pe.Process)
            .Include(pe => pe.Equipment)
            .Select(pe => new ProcessEquipmentDto
            {
                ProcessID = pe.ProcessID,
                EquipmentID = pe.EquipmentID,
                SequenceNo = pe.SequenceNo,
                IsRequired = pe.IsRequired,
                IsActive = pe.IsActive,
                ProcessName = pe.Process.ProcessName,
                EquipmentName = pe.Equipment.EquipmentName
            })
            .ToListAsync();
    }

    public async Task<List<ProcessEquipmentDto>> GetByEquipmentIdAsync(int equipmentId)
    {
        return await _context.ProcessEquipments
            .Where(pe => pe.EquipmentID == equipmentId)
            .Include(pe => pe.Process)
            .Include(pe => pe.Equipment)
            .Select(pe => new ProcessEquipmentDto
            {
                ProcessID = pe.ProcessID,
                EquipmentID = pe.EquipmentID,
                SequenceNo = pe.SequenceNo,
                IsRequired = pe.IsRequired,
                IsActive = pe.IsActive,
                ProcessName = pe.Process.ProcessName,
                EquipmentName = pe.Equipment.EquipmentName
            })
            .ToListAsync();
    }

    public async Task<ProcessEquipmentDto> LinkAsync(LinkProcessEquipmentDto dto)
    {
        var existing = await _context.ProcessEquipments
            .FindAsync(dto.ProcessID, dto.EquipmentID);
        if (existing != null)
            throw new InvalidOperationException("Link already exists.");

        var entity = new ProcessEquipment
        {
            ProcessID = dto.ProcessID,
            EquipmentID = dto.EquipmentID,
            SequenceNo = dto.SequenceNo,
            IsRequired = dto.IsRequired,
            IsActive = dto.IsActive
        };

        _context.ProcessEquipments.Add(entity);
        await _context.SaveChangesAsync();

        var process = await _context.Processes.FindAsync(dto.ProcessID);
        var equipment = await _context.Equipments.FindAsync(dto.EquipmentID);

        return new ProcessEquipmentDto
        {
            ProcessID = dto.ProcessID,
            EquipmentID = dto.EquipmentID,
            SequenceNo = dto.SequenceNo,
            IsRequired = dto.IsRequired,
            IsActive = dto.IsActive,
            ProcessName = process?.ProcessName,
            EquipmentName = equipment?.EquipmentName
        };
    }

    public async Task<bool> UnlinkAsync(int processId, int equipmentId)
    {
        var entity = await _context.ProcessEquipments.FindAsync(processId, equipmentId);
        if (entity == null) return false;

        _context.ProcessEquipments.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ProcessEquipmentDto?> UpdateAsync(int processId, int equipmentId, LinkProcessEquipmentDto dto)
    {
        var entity = await _context.ProcessEquipments.FindAsync(processId, equipmentId);
        if (entity == null) return null;

        entity.SequenceNo = dto.SequenceNo;
        entity.IsRequired = dto.IsRequired;
        entity.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        var process = await _context.Processes.FindAsync(processId);
        var equipment = await _context.Equipments.FindAsync(equipmentId);

        return new ProcessEquipmentDto
        {
            ProcessID = entity.ProcessID,
            EquipmentID = entity.EquipmentID,
            SequenceNo = entity.SequenceNo,
            IsRequired = entity.IsRequired,
            IsActive = entity.IsActive,
            ProcessName = process?.ProcessName,
            EquipmentName = equipment?.EquipmentName
        };
    }
}
