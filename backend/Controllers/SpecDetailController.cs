using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parason_Api.Models;
using Parason_Api.DTOs;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecDetailController : ControllerBase
{
    private readonly ParasonDbContext _context;

    public SpecDetailController(ParasonDbContext context)
    {
        _context = context;
    }

    // GET: api/SpecDetail
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SpecDetailDto>>> GetSpecDetails()
    {
        var specs = await _context.SpecDetails
            .Include(s => s.Attribute)
            .Include(s => s.Equipment)
            .Include(s => s.Model)
            .Include(s => s.ListValue)
            .Select(s => new SpecDetailDto
            {
                RecordId = s.RecordId,
                EquipmentId = s.EquipmentId,
                ModelId = s.ModelId,
                AttributeId = s.AttributeId,
                ListValueId = s.ListValueId,
                NumValue = s.NumValue,
                TextValue = s.TextValue,
                BoolValue = s.BoolValue,
                AttributeName = s.Attribute.AttributeName,
                EquipmentName = s.Equipment != null ? s.Equipment.EquipmentName : null,
                ModelName = s.Model != null ? s.Model.ModelName : null,
                ListValueDisplay = s.ListValue != null ? s.ListValue.Display : null
            })
            .ToListAsync();

        return Ok(specs);
    }

    // GET: api/SpecDetail/5/10
    [HttpGet("{recordId}/{attributeId}")]
    public async Task<ActionResult<SpecDetailDto>> GetSpecDetail(int recordId, int attributeId)
    {
        var spec = await _context.SpecDetails
            .Include(s => s.Attribute)
            .Include(s => s.Equipment)
            .Include(s => s.Model)
            .Include(s => s.ListValue)
            .Where(s => s.RecordId == recordId && s.AttributeId == attributeId)
            .Select(s => new SpecDetailDto
            {
                RecordId = s.RecordId,
                EquipmentId = s.EquipmentId,
                ModelId = s.ModelId,
                AttributeId = s.AttributeId,
                ListValueId = s.ListValueId,
                NumValue = s.NumValue,
                TextValue = s.TextValue,
                BoolValue = s.BoolValue,
                AttributeName = s.Attribute.AttributeName,
                EquipmentName = s.Equipment != null ? s.Equipment.EquipmentName : null,
                ModelName = s.Model != null ? s.Model.ModelName : null,
                ListValueDisplay = s.ListValue != null ? s.ListValue.Display : null
            })
            .FirstOrDefaultAsync();

        if (spec == null)
        {
            return NotFound();
        }

        return Ok(spec);
    }

    // GET: api/SpecDetail/ByRecord/5
    [HttpGet("ByRecord/{recordId}")]
    public async Task<ActionResult<IEnumerable<SpecDetailDto>>> GetSpecDetailsByRecord(int recordId)
    {
        var specs = await _context.SpecDetails
            .Where(s => s.RecordId == recordId)
            .Include(s => s.Attribute)
            .Include(s => s.Equipment)
            .Include(s => s.Model)
            .Include(s => s.ListValue)
            .Select(s => new SpecDetailDto
            {
                RecordId = s.RecordId,
                EquipmentId = s.EquipmentId,
                ModelId = s.ModelId,
                AttributeId = s.AttributeId,
                ListValueId = s.ListValueId,
                NumValue = s.NumValue,
                TextValue = s.TextValue,
                BoolValue = s.BoolValue,
                AttributeName = s.Attribute.AttributeName,
                EquipmentName = s.Equipment != null ? s.Equipment.EquipmentName : null,
                ModelName = s.Model != null ? s.Model.ModelName : null,
                ListValueDisplay = s.ListValue != null ? s.ListValue.Display : null
            })
            .ToListAsync();

        return Ok(specs);
    }

    // POST: api/SpecDetail
    [HttpPost]
    public async Task<ActionResult<SpecDetailDto>> CreateSpecDetail(CreateSpecDetailDto dto)
    {
        var spec = new SpecDetail
        {
            RecordId = dto.RecordId,
            EquipmentId = dto.EquipmentId,
            ModelId = dto.ModelId,
            AttributeId = dto.AttributeId,
            ListValueId = dto.ListValueId,
            NumValue = dto.NumValue,
            TextValue = dto.TextValue,
            BoolValue = dto.BoolValue
        };

        _context.SpecDetails.Add(spec);
        await _context.SaveChangesAsync();

        var attribute = await _context.AttributeDefs.FindAsync(dto.AttributeId);
        var equipment = dto.EquipmentId.HasValue ? await _context.Equipment.FindAsync(dto.EquipmentId) : null;
        var model = dto.ModelId.HasValue ? await _context.Models.FindAsync(dto.ModelId) : null;
        var listValue = dto.ListValueId.HasValue ? await _context.AttributeListValues.FindAsync(dto.ListValueId) : null;

        var resultDto = new SpecDetailDto
        {
            RecordId = spec.RecordId,
            EquipmentId = spec.EquipmentId,
            ModelId = spec.ModelId,
            AttributeId = spec.AttributeId,
            ListValueId = spec.ListValueId,
            NumValue = spec.NumValue,
            TextValue = spec.TextValue,
            BoolValue = spec.BoolValue,
            AttributeName = attribute?.AttributeName,
            EquipmentName = equipment?.EquipmentName,
            ModelName = model?.ModelName,
            ListValueDisplay = listValue?.Display
        };

        return CreatedAtAction(nameof(GetSpecDetail), new { recordId = spec.RecordId, attributeId = spec.AttributeId }, resultDto);
    }

    // PUT: api/SpecDetail/5/10
    [HttpPut("{recordId}/{attributeId}")]
    public async Task<IActionResult> UpdateSpecDetail(int recordId, int attributeId, UpdateSpecDetailDto dto)
    {
        var spec = await _context.SpecDetails.FindAsync(recordId, attributeId);

        if (spec == null)
        {
            return NotFound();
        }

        spec.EquipmentId = dto.EquipmentId;
        spec.ModelId = dto.ModelId;
        spec.ListValueId = dto.ListValueId;
        spec.NumValue = dto.NumValue;
        spec.TextValue = dto.TextValue;
        spec.BoolValue = dto.BoolValue;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/SpecDetail/5/10
    [HttpDelete("{recordId}/{attributeId}")]
    public async Task<IActionResult> DeleteSpecDetail(int recordId, int attributeId)
    {
        var spec = await _context.SpecDetails.FindAsync(recordId, attributeId);

        if (spec == null)
        {
            return NotFound();
        }

        _context.SpecDetails.Remove(spec);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
