using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Parason_Api.DTOs;
using Parason_Api.Models;

namespace Parason_Api.Services
{
    public class ProcessService : IProcessService
    {
        private readonly CPQDbContext _context;

        public ProcessService(CPQDbContext context)
        {
            this._context = context;
        }

        public async Task<ProcessDto> CreateAsync(CreateProcessDto dto, string createdBy)
        {
            var process = new Process
            {
                ProcessCode = dto.ProcessCode,
                ProcessName = dto.ProcessName,
                Description = dto.Description,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            _context.Processes.Add(process);
            await _context.SaveChangesAsync();

            return new ProcessDto
            {
                ProcessID = process.ProcessID,
                ProcessCode = process.ProcessCode,
                ProcessName = process.ProcessName,
                Description = process.Description,
                IsActive = process.IsActive
            };
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

        public async Task<List<ProcessDto>> GetAllAsync()
        {
            var query = _context.Processes.AsQueryable();

            return await _context.Processes
               .Select(p => new ProcessDto
               {
                   ProcessID = p.ProcessID,
                   ProcessCode = p.ProcessCode,
                   ProcessName = p.ProcessName,
                   Description =  p.Description,
                   IsActive =     p.IsActive
               }).ToListAsync(); 
        }

        public async Task<ProcessDto?> GetByIdAsync(int id)
        {
            var process = await _context.Processes
               .Where(v => v.ProcessID == id)
               .Select(v => new ProcessDto
               {
                   ProcessID = v.ProcessID,
                   ProcessCode = v.ProcessCode,
                   ProcessName = v.ProcessName,
                   Description = v.Description,
                   IsActive = v.IsActive
               })
               .FirstOrDefaultAsync();

            return process;
        }

        public Task<List<EquipmentDto>> GetEquipmentsByProcessAsync(int processId)
        {
            var equipments = _context.ProcessEquipments
                            .Where(e => e.IsActive && e.ProcessID == processId)
                            .Include(e => e.Equipment)
                            .ThenInclude(eq => eq.Series)
                            .Select(e => new EquipmentDto
                            {
                                EquipmentID = e.EquipmentID,
                                EquipmentCode = e.Equipment.EquipmentCode,
                                EquipmentName = e.Equipment.EquipmentName,
                                Description = e.Equipment.Description,
                                IsActive = e.IsActive,
                                Series = e.Equipment.Series
                                        .Select(s => new SeriesDto 
                                        { 
                                            SeriesID = s.SeriesID,
                                            SeriesName = s.SeriesName,
                                            Description = s.Description,
                                            SeriesCode = s.SeriesCode,
                                            IsActive = s.IsActive
                                        }).ToList()
                            }).ToListAsync();
            return equipments;
        }

        public Task<List<ProcessDto>> GetProcessesByVerticalAsync(int verticalId)
        {
            throw new NotImplementedException();
        }

        public async Task<ProcessDto?> UpdateAsync(int id, UpdateProcessDto dto, string modifiedBy)
        {
            var process = await _context.Processes.FindAsync(id);

            if (process == null)
                return null;

            process.ProcessName = dto.ProcessName;
            process.Description = dto.Description;
            process.IsActive = dto.IsActive;
            process.ProcessCode = dto.ProcessCode;

            await _context.SaveChangesAsync();

            return new ProcessDto
            {
                ProcessID= process.ProcessID,
                ProcessName = process.ProcessName,
                ProcessCode = process.ProcessCode,
                Description = process.Description,
                IsActive = process.IsActive
            };            
        }         
    }
}
