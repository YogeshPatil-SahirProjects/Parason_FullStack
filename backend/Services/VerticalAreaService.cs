using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;

namespace Parason_Api.Services
{
    public class VerticalAreaService : IVerticalAreaService
    {
        private readonly CPQDbContext _context;

        public VerticalAreaService(CPQDbContext context)
        {
            _context = context;
        }

        public async Task<List<VerticalAreaDto>> GetAllAsync()
        {
            return await _context.VerticalAreas
                .Select(v => new VerticalAreaDto
                {
                    VerticalID = v.VerticalID,
                    VerticalCode = v.VerticalCode,
                    VerticalName = v.VerticalName,
                    Description = v.Description,
                    IsActive = v.IsActive
                }).ToListAsync();
        }

        public async Task<VerticalAreaDto?> GetByIdAsync(int id)
        {
            var vertical = await _context.VerticalAreas
                .Where(v => v.VerticalID == id)
                .Select(v => new VerticalAreaDto
                {
                    VerticalID = v.VerticalID,
                    VerticalCode = v.VerticalCode,
                    VerticalName = v.VerticalName,
                    Description = v.Description,
                    IsActive = v.IsActive
                })
                .FirstOrDefaultAsync();

            return vertical;
        }

        public async Task<VerticalAreaDto> CreateAsync(CreateVerticalAreaDto dto, string createdBy)
        {
            var vertical = new VerticalArea
            {
                VerticalCode = dto.VerticalCode,
                VerticalName = dto.VerticalName,
                Description = dto.Description,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            _context.VerticalAreas.Add(vertical);
            await _context.SaveChangesAsync();

            return new VerticalAreaDto
            {
                VerticalID = vertical.VerticalID,
                VerticalCode = vertical.VerticalCode,
                VerticalName = vertical.VerticalName,
                Description = vertical.Description,
                IsActive = vertical.IsActive
            };
        }

        public async Task<VerticalAreaDto?> UpdateAsync(int id, UpdateVerticalAreaDto dto, string modifiedBy)
        {
            var vertical = await _context.VerticalAreas.FindAsync(id);
            if (vertical == null)
                return null;

            vertical.VerticalCode = dto.VerticalCode;
            vertical.VerticalName = dto.VerticalName;
            vertical.Description = dto.Description;
            vertical.IsActive = dto.IsActive;
            vertical.ModifiedAt = DateTime.UtcNow;
            vertical.ModifiedBy = modifiedBy;

            await _context.SaveChangesAsync();

            return new VerticalAreaDto
            {
                VerticalID = vertical.VerticalID,
                VerticalCode = vertical.VerticalCode,
                VerticalName = vertical.VerticalName,
                Description = vertical.Description,
                IsActive = vertical.IsActive
            };
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

        public async Task<List<CatalogVerticalDto>> GetVerticalCatalogAsync()
        {
            var verticals = await _context.VerticalAreas
                .Where(v => v.IsActive)
                .Include(v => v.VerticalProcesses)
                    .ThenInclude(vp => vp.Process)
                .OrderBy(v => v.VerticalName)
                .Select(v => new CatalogVerticalDto
                {
                    VerticalID = v.VerticalID,
                    VerticalCode = v.VerticalCode,
                    VerticalName = v.VerticalName,
                    Description = v.Description,
                    Processes = v.VerticalProcesses
                        .Where(vp => vp.IsActive && vp.Process.IsActive)
                        .OrderBy(vp => vp.SequenceNo)
                        .Select(vp => new CatalogProcessDto
                        {
                            ProcessID = vp.Process.ProcessID,
                            ProcessCode = vp.Process.ProcessCode,
                            ProcessName = vp.Process.ProcessName,
                            Description = vp.Process.Description,
                            SequenceNo = vp.SequenceNo,
                            IsRequired = vp.IsRequired
                        })
                        .ToList()
                })
                .ToListAsync();

            return verticals;
        }

        public async Task<List<ProcessDto>> GetProcessForSelectedVerticalAsync(int id)
        {
            var Processes = await _context.VerticalProcesses
                            .Where(v => v.IsActive && v.VerticalID == id)
                            .Include(v => v.Process)                             
                            .Select(v => new ProcessDto { 
                                ProcessID = v.ProcessID,
                                ProcessCode = v.Process.ProcessCode,
                                Description = v.Process.Description,
                                IsActive = v.Process.IsActive,
                                ProcessName = v.Process.ProcessName
                            }).ToListAsync();

            return Processes;
        }
    }

}
