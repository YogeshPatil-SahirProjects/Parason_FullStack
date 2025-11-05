using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parason_Api.Models;
using Parason_Api.DTOs;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeriesController : ControllerBase
{
    private readonly ParasonDbContext _context;

    public SeriesController(ParasonDbContext context)
    {
        _context = context;
    }

    // GET: api/Series
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SeriesDto>>> GetSeries()
    {
        var series = await _context.Series
            .Include(s => s.Equipment)
            .Select(s => new SeriesDto
            {
                SeriesId = s.SeriesId,
                EquipmentId = s.EquipmentId,
                SeriesCode = s.SeriesCode,
                SeriesName = s.SeriesName,
                Description = s.Description,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                ModifiedAt = s.ModifiedAt,
                ModifiedBy = s.ModifiedBy,
                EquipmentName = s.Equipment.EquipmentName
            })
            .ToListAsync();

        return Ok(series);
    }

    // GET: api/Series/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SeriesDto>> GetSeries(int id)
    {
        var series = await _context.Series
            .Include(s => s.Equipment)
            .Where(s => s.SeriesId == id)
            .Select(s => new SeriesDto
            {
                SeriesId = s.SeriesId,
                EquipmentId = s.EquipmentId,
                SeriesCode = s.SeriesCode,
                SeriesName = s.SeriesName,
                Description = s.Description,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                ModifiedAt = s.ModifiedAt,
                ModifiedBy = s.ModifiedBy,
                EquipmentName = s.Equipment.EquipmentName
            })
            .FirstOrDefaultAsync();

        if (series == null)
        {
            return NotFound();
        }

        return Ok(series);
    }

    // GET: api/Series/ByEquipment/5
    [HttpGet("ByEquipment/{equipmentId}")]
    public async Task<ActionResult<IEnumerable<SeriesDto>>> GetSeriesByEquipment(int equipmentId)
    {
        var series = await _context.Series
            .Where(s => s.EquipmentId == equipmentId)
            .Include(s => s.Equipment)
            .Select(s => new SeriesDto
            {
                SeriesId = s.SeriesId,
                EquipmentId = s.EquipmentId,
                SeriesCode = s.SeriesCode,
                SeriesName = s.SeriesName,
                Description = s.Description,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                ModifiedAt = s.ModifiedAt,
                ModifiedBy = s.ModifiedBy,
                EquipmentName = s.Equipment.EquipmentName
            })
            .ToListAsync();

        return Ok(series);
    }

    // POST: api/Series
    [HttpPost]
    public async Task<ActionResult<SeriesDto>> CreateSeries(CreateSeriesDto dto)
    {
        var series = new Series
        {
            EquipmentId = dto.EquipmentId,
            SeriesCode = dto.SeriesCode,
            SeriesName = dto.SeriesName,
            Description = dto.Description,
            IsActive = dto.IsActive
        };

        _context.Series.Add(series);
        await _context.SaveChangesAsync();

        var equipment = await _context.Equipment.FindAsync(dto.EquipmentId);

        var resultDto = new SeriesDto
        {
            SeriesId = series.SeriesId,
            EquipmentId = series.EquipmentId,
            SeriesCode = series.SeriesCode,
            SeriesName = series.SeriesName,
            Description = series.Description,
            IsActive = series.IsActive,
            CreatedAt = series.CreatedAt,
            CreatedBy = series.CreatedBy,
            ModifiedAt = series.ModifiedAt,
            ModifiedBy = series.ModifiedBy,
            EquipmentName = equipment?.EquipmentName
        };

        return CreatedAtAction(nameof(GetSeries), new { id = series.SeriesId }, resultDto);
    }

    // PUT: api/Series/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSeries(int id, UpdateSeriesDto dto)
    {
        var series = await _context.Series.FindAsync(id);

        if (series == null)
        {
            return NotFound();
        }

        series.EquipmentId = dto.EquipmentId;
        series.SeriesCode = dto.SeriesCode;
        series.SeriesName = dto.SeriesName;
        series.Description = dto.Description;
        series.IsActive = dto.IsActive;
        series.ModifiedAt = DateTime.UtcNow;
        series.ModifiedBy = "System"; // TODO: Get from authenticated user

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Series/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSeries(int id)
    {
        var series = await _context.Series.FindAsync(id);

        if (series == null)
        {
            return NotFound();
        }

        _context.Series.Remove(series);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
