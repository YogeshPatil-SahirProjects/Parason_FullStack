using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Services;

public class ProcessService : IProcessService
{
    private readonly ParasonDbContext _context;

    public ProcessService(ParasonDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProcessDto>> GetAllAsync()
    {
        return await _context.Processes
            .Select(p => MapToDto(p))
            .ToListAsync();
    }

    public async Task<ProcessDto?> GetByIdAsync(int id)
    {
        var process = await _context.Processes.FindAsync(id);
        return process == null ? null : MapToDto(process);
    }

    public async Task<ProcessDto> CreateAsync(CreateProcessDto dto)
    {
        var process = new Process
        {
            ProcessCode = dto.ProcessCode,
            ProcessName = dto.ProcessName,
            Description = dto.Description,
            IsActive = dto.IsActive
        };

        _context.Processes.Add(process);
        await _context.SaveChangesAsync();

        return MapToDto(process);
    }

    public async Task<bool> UpdateAsync(int id, UpdateProcessDto dto)
    {
        var process = await _context.Processes.FindAsync(id);

        if (process == null)
            return false;

        process.ProcessCode = dto.ProcessCode;
        process.ProcessName = dto.ProcessName;
        process.Description = dto.Description;
        process.IsActive = dto.IsActive;
        process.ModifiedAt = DateTime.UtcNow;
        process.ModifiedBy = "System";

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var process = await _context.Processes.FindAsync(id);

        if (process == null)
            return false;

        _context.Processes.Remove(process);
        await _context.SaveChangesAsync();
        return true;
    }

    private static ProcessDto MapToDto(Process process)
    {
        return new ProcessDto
        {
            ProcessId = process.ProcessId,
            ProcessCode = process.ProcessCode,
            ProcessName = process.ProcessName,
            Description = process.Description,
            IsActive = process.IsActive,
            CreatedAt = process.CreatedAt,
            CreatedBy = process.CreatedBy,
            ModifiedAt = process.ModifiedAt,
            ModifiedBy = process.ModifiedBy
        };
    }
}
