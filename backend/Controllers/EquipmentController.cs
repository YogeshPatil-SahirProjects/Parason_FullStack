using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parason_Api.Models;
using Parason_Api.DTOs;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly ParasonDbContext _context;

    public EquipmentController(ParasonDbContext context)
    {
        _context = context;
    }

    // GET: api/Equipment
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetEquipment()
    {
        var equipment = await _context.Equipment
            .Select(e => new EquipmentDto
            {
                EquipmentId = e.EquipmentId,
                EquipmentCode = e.EquipmentCode,
                EquipmentName = e.EquipmentName,
                Description = e.Description,
                IsActive = e.IsActive,
                CreatedAt = e.CreatedAt,
                CreatedBy = e.CreatedBy,
                ModifiedAt = e.ModifiedAt,
                ModifiedBy = e.ModifiedBy
            })
            .ToListAsync();

        return Ok(equipment);
    }

    // GET: api/Equipment/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EquipmentDto>> GetEquipment(int id)
    {
        var equipment = await _context.Equipment
            .Where(e => e.EquipmentId == id)
            .Select(e => new EquipmentDto
            {
                EquipmentId = e.EquipmentId,
                EquipmentCode = e.EquipmentCode,
                EquipmentName = e.EquipmentName,
                Description = e.Description,
                IsActive = e.IsActive,
                CreatedAt = e.CreatedAt,
                CreatedBy = e.CreatedBy,
                ModifiedAt = e.ModifiedAt,
                ModifiedBy = e.ModifiedBy
            })
            .FirstOrDefaultAsync();

        if (equipment == null)
        {
            return NotFound();
        }

        return Ok(equipment);
    }

    // POST: api/Equipment
    [HttpPost]
    public async Task<ActionResult<EquipmentDto>> CreateEquipment(CreateEquipmentDto dto)
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

        var resultDto = new EquipmentDto
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

        return CreatedAtAction(nameof(GetEquipment), new { id = equipment.EquipmentId }, resultDto);
    }

    // PUT: api/Equipment/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEquipment(int id, UpdateEquipmentDto dto)
    {
        var equipment = await _context.Equipment.FindAsync(id);

        if (equipment == null)
        {
            return NotFound();
        }

        equipment.EquipmentCode = dto.EquipmentCode;
        equipment.EquipmentName = dto.EquipmentName;
        equipment.Description = dto.Description;
        equipment.IsActive = dto.IsActive;
        equipment.ModifiedAt = DateTime.UtcNow;
        equipment.ModifiedBy = "System"; // TODO: Get from authenticated user

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Equipment/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEquipment(int id)
    {
        var equipment = await _context.Equipment.FindAsync(id);

        if (equipment == null)
        {
            return NotFound();
        }

        _context.Equipment.Remove(equipment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
