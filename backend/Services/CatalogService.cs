using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Services;

public class CatalogService : ICatalogService
{
    private readonly CPQDbContext _context;

    public CatalogService(CPQDbContext context)
    {
        _context = context;
    }

    public async Task<List<CatalogVerticalDto>> GetCatalogHierarchyAsync()
    {
        var verticals = await _context.VerticalAreas
            .Include(v => v.VerticalProcesses)
                .ThenInclude(vp => vp.Process)
                    .ThenInclude(p => p.ProcessEquipments)
                        .ThenInclude(pe => pe.Equipment)
            .ToListAsync();

        return verticals.Select(v => new CatalogVerticalDto
        {
            VerticalID = v.VerticalID,
            VerticalCode = v.VerticalCode,
            VerticalName = v.VerticalName,
            Description = v.Description,
            Processes = v.VerticalProcesses?.Select(vp => new CatalogProcessDto
            {
                ProcessID = vp.ProcessID,
                ProcessCode = vp.Process.ProcessCode,
                ProcessName = vp.Process.ProcessName,
                Description = vp.Process.Description,
                SequenceNo = vp.SequenceNo,
                IsRequired = vp.IsRequired,
                Equipments = vp.Process.ProcessEquipments?.Select(pe => new CatalogEquipmentDto
                {
                    EquipmentID = pe.EquipmentID,
                    EquipmentCode = pe.Equipment.EquipmentCode,
                    EquipmentName = pe.Equipment.EquipmentName,
                    SequenceNo = pe.SequenceNo,
                    IsRequired = pe.IsRequired
                }).ToList()
            }).ToList()
        }).ToList();
    }

    public async Task<CatalogVerticalDto?> GetVerticalWithProcessesAsync(int verticalId)
    {
        var vertical = await _context.VerticalAreas
            .Include(v => v.VerticalProcesses)
                .ThenInclude(vp => vp.Process)
            .FirstOrDefaultAsync(v => v.VerticalID == verticalId);

        if (vertical == null) return null;

        return new CatalogVerticalDto
        {
            VerticalID = vertical.VerticalID,
            VerticalCode = vertical.VerticalCode,
            VerticalName = vertical.VerticalName,
            Description = vertical.Description,
            Processes = vertical.VerticalProcesses?.Select(vp => new CatalogProcessDto
            {
                ProcessID = vp.ProcessID,
                ProcessCode = vp.Process.ProcessCode,
                ProcessName = vp.Process.ProcessName,
                Description = vp.Process.Description,
                SequenceNo = vp.SequenceNo,
                IsRequired = vp.IsRequired
            }).ToList()
        };
    }

    public async Task<CatalogProcessDto?> GetProcessWithEquipmentsAsync(int processId)
    {
        var process = await _context.Processes
            .Include(p => p.ProcessEquipments)
                .ThenInclude(pe => pe.Equipment)
            .FirstOrDefaultAsync(p => p.ProcessID == processId);

        if (process == null) return null;

        return new CatalogProcessDto
        {
            ProcessID = process.ProcessID,
            ProcessCode = process.ProcessCode,
            ProcessName = process.ProcessName,
            Description = process.Description,
            Equipments = process.ProcessEquipments?.Select(pe => new CatalogEquipmentDto
            {
                EquipmentID = pe.EquipmentID,
                EquipmentCode = pe.Equipment.EquipmentCode,
                EquipmentName = pe.Equipment.EquipmentName,
                SequenceNo = pe.SequenceNo,
                IsRequired = pe.IsRequired
            }).ToList()
        };
    }
}
