using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services;

public class VerticalProcessService : IVerticalProcessService
{
    private readonly CPQDbContext _context;

    public VerticalProcessService(CPQDbContext context)
    {
        _context = context;
    }

    public async Task<List<VerticalProcessDto>> GetByVerticalIdAsync(int verticalId)
    {
        return await _context.VerticalProcesses
            .Where(lp => lp.VerticalID == verticalId)
            .Include(lp => lp.VerticalArea)
            .Include(lp => lp.Process)
            .Select(lp => new VerticalProcessDto
            {
                VerticalID = lp.VerticalID,
                ProcessID = lp.ProcessID,
                SequenceNo = lp.SequenceNo,
                IsRequired = lp.IsRequired,
                IsActive = lp.IsActive,
                VerticalName = lp.VerticalArea.VerticalName,
                ProcessName = lp.Process.ProcessName
            })
            .ToListAsync();
    }

    public async Task<List<VerticalProcessDto>> GetByProcessIdAsync(int processId)
    {
        return await _context.VerticalProcesses
            .Where(lp => lp.ProcessID == processId)
            .Include(lp => lp.VerticalArea)
            .Include(lp => lp.Process)
            .Select(lp => new VerticalProcessDto
            {
                VerticalID = lp.VerticalID,
                ProcessID = lp.ProcessID,
                SequenceNo = lp.SequenceNo,
                IsRequired = lp.IsRequired,
                IsActive = lp.IsActive,
                VerticalName = lp.VerticalArea.VerticalName,
                ProcessName = lp.Process.ProcessName
            })
            .ToListAsync();
    }

    public async Task<VerticalProcessDto> LinkAsync(LinkVerticalProcessDto dto)
    {
        var entity = await _context.VerticalProcesses
            .FindAsync(dto.VerticalID, dto.ProcessID);

        if (entity != null)
            throw new InvalidOperationException("Link already exists.");

        entity = new VerticalProcess
        {
            VerticalID = dto.VerticalID,
            ProcessID = dto.ProcessID,
            SequenceNo = dto.SequenceNo,
            IsRequired = dto.IsRequired,
            IsActive = dto.IsActive
        };

        _context.VerticalProcesses.Add(entity);
        await _context.SaveChangesAsync();

        var vertical = await _context.VerticalAreas.FindAsync(dto.VerticalID);
        var process = await _context.Processes.FindAsync(dto.ProcessID);

        return new VerticalProcessDto
        {
            VerticalID = dto.VerticalID,
            ProcessID = dto.ProcessID,
            SequenceNo = dto.SequenceNo,
            IsRequired = dto.IsRequired,
            IsActive = dto.IsActive,
            VerticalName = vertical?.VerticalName,
            ProcessName = process?.ProcessName
        };
    }

    public async Task<bool> UnlinkAsync(int verticalId, int processId)
    {
        var entity = await _context.VerticalProcesses.FindAsync(verticalId, processId);
        if (entity == null) return false;

        _context.VerticalProcesses.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<VerticalProcessDto?> UpdateAsync(int verticalId, int processId, LinkVerticalProcessDto dto)
    {
        var entity = await _context.VerticalProcesses.FindAsync(verticalId, processId);
        if (entity == null) return null;

        entity.SequenceNo = dto.SequenceNo;
        entity.IsRequired = dto.IsRequired;
        entity.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        var vertical = await _context.VerticalAreas.FindAsync(verticalId);
        var process = await _context.Processes.FindAsync(processId);

        return new VerticalProcessDto
        {
            VerticalID = entity.VerticalID,
            ProcessID = entity.ProcessID,
            SequenceNo = entity.SequenceNo,
            IsRequired = entity.IsRequired,
            IsActive = entity.IsActive,
            VerticalName = vertical?.VerticalName,
            ProcessName = process?.ProcessName
        };
    }
}
