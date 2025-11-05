using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parason_Api.Models;
using Parason_Api.DTOs;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuoteEquipmentOrModelController : ControllerBase
{
    private readonly ParasonDbContext _context;

    public QuoteEquipmentOrModelController(ParasonDbContext context)
    {
        _context = context;
    }

    // GET: api/QuoteEquipmentOrModel
    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuoteEquipmentOrModelDto>>> GetQuoteEquipmentOrModels()
    {
        var items = await _context.QuoteEquipmentOrModels
            .Include(q => q.Equipment)
            .Include(q => q.Series)
            .Include(q => q.Model)
            .Select(q => new QuoteEquipmentOrModelDto
            {
                Qeomid = q.Qeomid,
                RecordId = q.RecordId,
                EquipmentId = q.EquipmentId,
                SeriesId = q.SeriesId,
                ModelId = q.ModelId,
                PriceInr = q.PriceInr,
                Quantity = q.Quantity,
                EquipmentName = q.Equipment != null ? q.Equipment.EquipmentName : null,
                SeriesName = q.Series != null ? q.Series.SeriesName : null,
                ModelName = q.Model != null ? q.Model.ModelName : null
            })
            .ToListAsync();

        return Ok(items);
    }

    // GET: api/QuoteEquipmentOrModel/5
    [HttpGet("{id}")]
    public async Task<ActionResult<QuoteEquipmentOrModelDto>> GetQuoteEquipmentOrModel(int id)
    {
        var item = await _context.QuoteEquipmentOrModels
            .Include(q => q.Equipment)
            .Include(q => q.Series)
            .Include(q => q.Model)
            .Where(q => q.Qeomid == id)
            .Select(q => new QuoteEquipmentOrModelDto
            {
                Qeomid = q.Qeomid,
                RecordId = q.RecordId,
                EquipmentId = q.EquipmentId,
                SeriesId = q.SeriesId,
                ModelId = q.ModelId,
                PriceInr = q.PriceInr,
                Quantity = q.Quantity,
                EquipmentName = q.Equipment != null ? q.Equipment.EquipmentName : null,
                SeriesName = q.Series != null ? q.Series.SeriesName : null,
                ModelName = q.Model != null ? q.Model.ModelName : null
            })
            .FirstOrDefaultAsync();

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    // GET: api/QuoteEquipmentOrModel/ByRecord/5
    [HttpGet("ByRecord/{recordId}")]
    public async Task<ActionResult<IEnumerable<QuoteEquipmentOrModelDto>>> GetQuoteEquipmentOrModelsByRecord(int recordId)
    {
        var items = await _context.QuoteEquipmentOrModels
            .Where(q => q.RecordId == recordId)
            .Include(q => q.Equipment)
            .Include(q => q.Series)
            .Include(q => q.Model)
            .Select(q => new QuoteEquipmentOrModelDto
            {
                Qeomid = q.Qeomid,
                RecordId = q.RecordId,
                EquipmentId = q.EquipmentId,
                SeriesId = q.SeriesId,
                ModelId = q.ModelId,
                PriceInr = q.PriceInr,
                Quantity = q.Quantity,
                EquipmentName = q.Equipment != null ? q.Equipment.EquipmentName : null,
                SeriesName = q.Series != null ? q.Series.SeriesName : null,
                ModelName = q.Model != null ? q.Model.ModelName : null
            })
            .ToListAsync();

        return Ok(items);
    }

    // POST: api/QuoteEquipmentOrModel
    [HttpPost]
    public async Task<ActionResult<QuoteEquipmentOrModelDto>> CreateQuoteEquipmentOrModel(CreateQuoteEquipmentOrModelDto dto)
    {
        var item = new QuoteEquipmentOrModel
        {
            RecordId = dto.RecordId,
            EquipmentId = dto.EquipmentId,
            SeriesId = dto.SeriesId,
            ModelId = dto.ModelId,
            PriceInr = dto.PriceInr,
            Quantity = dto.Quantity
        };

        _context.QuoteEquipmentOrModels.Add(item);
        await _context.SaveChangesAsync();

        var equipment = dto.EquipmentId.HasValue ? await _context.Equipment.FindAsync(dto.EquipmentId) : null;
        var series = dto.SeriesId.HasValue ? await _context.Series.FindAsync(dto.SeriesId) : null;
        var model = dto.ModelId.HasValue ? await _context.Models.FindAsync(dto.ModelId) : null;

        var resultDto = new QuoteEquipmentOrModelDto
        {
            Qeomid = item.Qeomid,
            RecordId = item.RecordId,
            EquipmentId = item.EquipmentId,
            SeriesId = item.SeriesId,
            ModelId = item.ModelId,
            PriceInr = item.PriceInr,
            Quantity = item.Quantity,
            EquipmentName = equipment?.EquipmentName,
            SeriesName = series?.SeriesName,
            ModelName = model?.ModelName
        };

        return CreatedAtAction(nameof(GetQuoteEquipmentOrModel), new { id = item.Qeomid }, resultDto);
    }

    // PUT: api/QuoteEquipmentOrModel/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuoteEquipmentOrModel(int id, UpdateQuoteEquipmentOrModelDto dto)
    {
        var item = await _context.QuoteEquipmentOrModels.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        item.RecordId = dto.RecordId;
        item.EquipmentId = dto.EquipmentId;
        item.SeriesId = dto.SeriesId;
        item.ModelId = dto.ModelId;
        item.PriceInr = dto.PriceInr;
        item.Quantity = dto.Quantity;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/QuoteEquipmentOrModel/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuoteEquipmentOrModel(int id)
    {
        var item = await _context.QuoteEquipmentOrModels.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        _context.QuoteEquipmentOrModels.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
