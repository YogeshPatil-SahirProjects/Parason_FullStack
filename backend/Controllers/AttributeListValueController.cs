using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parason_Api.Models;
using Parason_Api.DTOs;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttributeListValueController : ControllerBase
{
    private readonly ParasonDbContext _context;

    public AttributeListValueController(ParasonDbContext context)
    {
        _context = context;
    }

    // GET: api/AttributeListValue
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AttributeListValueDto>>> GetAttributeListValues()
    {
        var listValues = await _context.AttributeListValues
            .Include(l => l.Attribute)
            .Select(l => new AttributeListValueDto
            {
                ListValueId = l.ListValueId,
                AttributeId = l.AttributeId,
                AttributeValue = l.AttributeValue,
                Display = l.Display,
                SequenceNo = l.SequenceNo,
                AttributeName = l.Attribute.AttributeName
            })
            .ToListAsync();

        return Ok(listValues);
    }

    // GET: api/AttributeListValue/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AttributeListValueDto>> GetAttributeListValue(int id)
    {
        var listValue = await _context.AttributeListValues
            .Include(l => l.Attribute)
            .Where(l => l.ListValueId == id)
            .Select(l => new AttributeListValueDto
            {
                ListValueId = l.ListValueId,
                AttributeId = l.AttributeId,
                AttributeValue = l.AttributeValue,
                Display = l.Display,
                SequenceNo = l.SequenceNo,
                AttributeName = l.Attribute.AttributeName
            })
            .FirstOrDefaultAsync();

        if (listValue == null)
        {
            return NotFound();
        }

        return Ok(listValue);
    }

    // GET: api/AttributeListValue/ByAttribute/5
    [HttpGet("ByAttribute/{attributeId}")]
    public async Task<ActionResult<IEnumerable<AttributeListValueDto>>> GetListValuesByAttribute(int attributeId)
    {
        var listValues = await _context.AttributeListValues
            .Where(l => l.AttributeId == attributeId)
            .Include(l => l.Attribute)
            .Select(l => new AttributeListValueDto
            {
                ListValueId = l.ListValueId,
                AttributeId = l.AttributeId,
                AttributeValue = l.AttributeValue,
                Display = l.Display,
                SequenceNo = l.SequenceNo,
                AttributeName = l.Attribute.AttributeName
            })
            .OrderBy(l => l.SequenceNo)
            .ToListAsync();

        return Ok(listValues);
    }

    // POST: api/AttributeListValue
    [HttpPost]
    public async Task<ActionResult<AttributeListValueDto>> CreateAttributeListValue(CreateAttributeListValueDto dto)
    {
        var listValue = new AttributeListValue
        {
            AttributeId = dto.AttributeId,
            AttributeValue = dto.AttributeValue,
            Display = dto.Display,
            SequenceNo = dto.SequenceNo
        };

        _context.AttributeListValues.Add(listValue);
        await _context.SaveChangesAsync();

        var attribute = await _context.AttributeDefs.FindAsync(dto.AttributeId);

        var resultDto = new AttributeListValueDto
        {
            ListValueId = listValue.ListValueId,
            AttributeId = listValue.AttributeId,
            AttributeValue = listValue.AttributeValue,
            Display = listValue.Display,
            SequenceNo = listValue.SequenceNo,
            AttributeName = attribute?.AttributeName
        };

        return CreatedAtAction(nameof(GetAttributeListValue), new { id = listValue.ListValueId }, resultDto);
    }

    // PUT: api/AttributeListValue/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAttributeListValue(int id, UpdateAttributeListValueDto dto)
    {
        var listValue = await _context.AttributeListValues.FindAsync(id);

        if (listValue == null)
        {
            return NotFound();
        }

        listValue.AttributeId = dto.AttributeId;
        listValue.AttributeValue = dto.AttributeValue;
        listValue.Display = dto.Display;
        listValue.SequenceNo = dto.SequenceNo;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/AttributeListValue/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttributeListValue(int id)
    {
        var listValue = await _context.AttributeListValues.FindAsync(id);

        if (listValue == null)
        {
            return NotFound();
        }

        _context.AttributeListValues.Remove(listValue);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
