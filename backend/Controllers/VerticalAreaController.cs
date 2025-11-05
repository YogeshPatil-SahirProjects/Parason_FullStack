using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parason_Api.Models;
using Parason_Api.DTOs;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VerticalAreaController : ControllerBase
{
    private readonly ParasonDbContext _context;

    public VerticalAreaController(ParasonDbContext context)
    {
        _context = context;
    }

    // GET: api/VerticalArea
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VerticalAreaDto>>> GetVerticalAreas()
    {
        var verticalAreas = await _context.VerticalAreas
            .Select(v => new VerticalAreaDto
            {
                VerticalId = v.VerticalId,
                VerticalCode = v.VerticalCode,
                VerticalName = v.VerticalName,
                Description = v.Description,
                IsActive = v.IsActive,
                CreatedAt = v.CreatedAt,
                CreatedBy = v.CreatedBy,
                ModifiedAt = v.ModifiedAt,
                ModifiedBy = v.ModifiedBy
            })
            .ToListAsync();

        return Ok(verticalAreas);
    }

    // GET: api/VerticalArea/5
    [HttpGet("{id}")]
    public async Task<ActionResult<VerticalAreaDto>> GetVerticalArea(int id)
    {
        var verticalArea = await _context.VerticalAreas
            .Where(v => v.VerticalId == id)
            .Select(v => new VerticalAreaDto
            {
                VerticalId = v.VerticalId,
                VerticalCode = v.VerticalCode,
                VerticalName = v.VerticalName,
                Description = v.Description,
                IsActive = v.IsActive,
                CreatedAt = v.CreatedAt,
                CreatedBy = v.CreatedBy,
                ModifiedAt = v.ModifiedAt,
                ModifiedBy = v.ModifiedBy
            })
            .FirstOrDefaultAsync();

        if (verticalArea == null)
        {
            return NotFound();
        }

        return Ok(verticalArea);
    }

    // POST: api/VerticalArea
    [HttpPost]
    public async Task<ActionResult<VerticalAreaDto>> CreateVerticalArea(CreateVerticalAreaDto dto)
    {
        var verticalArea = new VerticalArea
        {
            VerticalCode = dto.VerticalCode,
            VerticalName = dto.VerticalName,
            Description = dto.Description,
            IsActive = dto.IsActive
        };

        _context.VerticalAreas.Add(verticalArea);
        await _context.SaveChangesAsync();

        var resultDto = new VerticalAreaDto
        {
            VerticalId = verticalArea.VerticalId,
            VerticalCode = verticalArea.VerticalCode,
            VerticalName = verticalArea.VerticalName,
            Description = verticalArea.Description,
            IsActive = verticalArea.IsActive,
            CreatedAt = verticalArea.CreatedAt,
            CreatedBy = verticalArea.CreatedBy,
            ModifiedAt = verticalArea.ModifiedAt,
            ModifiedBy = verticalArea.ModifiedBy
        };

        return CreatedAtAction(nameof(GetVerticalArea), new { id = verticalArea.VerticalId }, resultDto);
    }

    // PUT: api/VerticalArea/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVerticalArea(int id, UpdateVerticalAreaDto dto)
    {
        var verticalArea = await _context.VerticalAreas.FindAsync(id);

        if (verticalArea == null)
        {
            return NotFound();
        }

        verticalArea.VerticalCode = dto.VerticalCode;
        verticalArea.VerticalName = dto.VerticalName;
        verticalArea.Description = dto.Description;
        verticalArea.IsActive = dto.IsActive;
        verticalArea.ModifiedAt = DateTime.UtcNow;
        verticalArea.ModifiedBy = "System"; // TODO: Get from authenticated user

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/VerticalArea/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVerticalArea(int id)
    {
        var verticalArea = await _context.VerticalAreas.FindAsync(id);

        if (verticalArea == null)
        {
            return NotFound();
        }

        _context.VerticalAreas.Remove(verticalArea);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
