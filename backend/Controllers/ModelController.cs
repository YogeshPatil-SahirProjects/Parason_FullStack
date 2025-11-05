using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parason_Api.Models;
using Parason_Api.DTOs;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModelController : ControllerBase
{
    private readonly ParasonDbContext _context;

    public ModelController(ParasonDbContext context)
    {
        _context = context;
    }

    // GET: api/Model
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ModelDto>>> GetModels()
    {
        var models = await _context.Models
            .Include(m => m.Series)
                .ThenInclude(s => s.Equipment)
            .Select(m => new ModelDto
            {
                ModelId = m.ModelId,
                SeriesId = m.SeriesId,
                ModelCode = m.ModelCode,
                ModelName = m.ModelName,
                Description = m.Description,
                IsActive = m.IsActive,
                CreatedAt = m.CreatedAt,
                CreatedBy = m.CreatedBy,
                ModifiedAt = m.ModifiedAt,
                ModifiedBy = m.ModifiedBy,
                SeriesName = m.Series.SeriesName,
                EquipmentName = m.Series.Equipment.EquipmentName
            })
            .ToListAsync();

        return Ok(models);
    }

    // GET: api/Model/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ModelDto>> GetModel(int id)
    {
        var model = await _context.Models
            .Include(m => m.Series)
                .ThenInclude(s => s.Equipment)
            .Where(m => m.ModelId == id)
            .Select(m => new ModelDto
            {
                ModelId = m.ModelId,
                SeriesId = m.SeriesId,
                ModelCode = m.ModelCode,
                ModelName = m.ModelName,
                Description = m.Description,
                IsActive = m.IsActive,
                CreatedAt = m.CreatedAt,
                CreatedBy = m.CreatedBy,
                ModifiedAt = m.ModifiedAt,
                ModifiedBy = m.ModifiedBy,
                SeriesName = m.Series.SeriesName,
                EquipmentName = m.Series.Equipment.EquipmentName
            })
            .FirstOrDefaultAsync();

        if (model == null)
        {
            return NotFound();
        }

        return Ok(model);
    }

    // GET: api/Model/BySeries/5
    [HttpGet("BySeries/{seriesId}")]
    public async Task<ActionResult<IEnumerable<ModelDto>>> GetModelsBySeries(int seriesId)
    {
        var models = await _context.Models
            .Where(m => m.SeriesId == seriesId)
            .Include(m => m.Series)
                .ThenInclude(s => s.Equipment)
            .Select(m => new ModelDto
            {
                ModelId = m.ModelId,
                SeriesId = m.SeriesId,
                ModelCode = m.ModelCode,
                ModelName = m.ModelName,
                Description = m.Description,
                IsActive = m.IsActive,
                CreatedAt = m.CreatedAt,
                CreatedBy = m.CreatedBy,
                ModifiedAt = m.ModifiedAt,
                ModifiedBy = m.ModifiedBy,
                SeriesName = m.Series.SeriesName,
                EquipmentName = m.Series.Equipment.EquipmentName
            })
            .ToListAsync();

        return Ok(models);
    }

    // POST: api/Model
    [HttpPost]
    public async Task<ActionResult<ModelDto>> CreateModel(CreateModelDto dto)
    {
        var model = new Model
        {
            SeriesId = dto.SeriesId,
            ModelCode = dto.ModelCode,
            ModelName = dto.ModelName,
            Description = dto.Description,
            IsActive = dto.IsActive
        };

        _context.Models.Add(model);
        await _context.SaveChangesAsync();

        var series = await _context.Series
            .Include(s => s.Equipment)
            .FirstOrDefaultAsync(s => s.SeriesId == dto.SeriesId);

        var resultDto = new ModelDto
        {
            ModelId = model.ModelId,
            SeriesId = model.SeriesId,
            ModelCode = model.ModelCode,
            ModelName = model.ModelName,
            Description = model.Description,
            IsActive = model.IsActive,
            CreatedAt = model.CreatedAt,
            CreatedBy = model.CreatedBy,
            ModifiedAt = model.ModifiedAt,
            ModifiedBy = model.ModifiedBy,
            SeriesName = series?.SeriesName,
            EquipmentName = series?.Equipment?.EquipmentName
        };

        return CreatedAtAction(nameof(GetModel), new { id = model.ModelId }, resultDto);
    }

    // PUT: api/Model/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateModel(int id, UpdateModelDto dto)
    {
        var model = await _context.Models.FindAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        model.SeriesId = dto.SeriesId;
        model.ModelCode = dto.ModelCode;
        model.ModelName = dto.ModelName;
        model.Description = dto.Description;
        model.IsActive = dto.IsActive;
        model.ModifiedAt = DateTime.UtcNow;
        model.ModifiedBy = "System"; // TODO: Get from authenticated user

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Model/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteModel(int id)
    {
        var model = await _context.Models.FindAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        _context.Models.Remove(model);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
