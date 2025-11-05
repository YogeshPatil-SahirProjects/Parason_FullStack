using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parason_Api.Models;
using Parason_Api.DTOs;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProcessController : ControllerBase
{
    private readonly ParasonDbContext _context;

    public ProcessController(ParasonDbContext context)
    {
        _context = context;
    }

    // GET: api/Process
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProcessDto>>> GetProcesses()
    {
        var processes = await _context.Processes
            .Select(p => new ProcessDto
            {
                ProcessId = p.ProcessId,
                ProcessCode = p.ProcessCode,
                ProcessName = p.ProcessName,
                Description = p.Description,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                CreatedBy = p.CreatedBy,
                ModifiedAt = p.ModifiedAt,
                ModifiedBy = p.ModifiedBy
            })
            .ToListAsync();

        return Ok(processes);
    }

    // GET: api/Process/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProcessDto>> GetProcess(int id)
    {
        var process = await _context.Processes
            .Where(p => p.ProcessId == id)
            .Select(p => new ProcessDto
            {
                ProcessId = p.ProcessId,
                ProcessCode = p.ProcessCode,
                ProcessName = p.ProcessName,
                Description = p.Description,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                CreatedBy = p.CreatedBy,
                ModifiedAt = p.ModifiedAt,
                ModifiedBy = p.ModifiedBy
            })
            .FirstOrDefaultAsync();

        if (process == null)
        {
            return NotFound();
        }

        return Ok(process);
    }

    // POST: api/Process
    [HttpPost]
    public async Task<ActionResult<ProcessDto>> CreateProcess(CreateProcessDto dto)
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

        var resultDto = new ProcessDto
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

        return CreatedAtAction(nameof(GetProcess), new { id = process.ProcessId }, resultDto);
    }

    // PUT: api/Process/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProcess(int id, UpdateProcessDto dto)
    {
        var process = await _context.Processes.FindAsync(id);

        if (process == null)
        {
            return NotFound();
        }

        process.ProcessCode = dto.ProcessCode;
        process.ProcessName = dto.ProcessName;
        process.Description = dto.Description;
        process.IsActive = dto.IsActive;
        process.ModifiedAt = DateTime.UtcNow;
        process.ModifiedBy = "System"; // TODO: Get from authenticated user

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Process/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProcess(int id)
    {
        var process = await _context.Processes.FindAsync(id);

        if (process == null)
        {
            return NotFound();
        }

        _context.Processes.Remove(process);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
