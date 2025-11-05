using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parason_Api.Models;
using Parason_Api.DTOs;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttributeDefController : ControllerBase
{
    private readonly ParasonDbContext _context;

    public AttributeDefController(ParasonDbContext context)
    {
        _context = context;
    }

    // GET: api/AttributeDef
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AttributeDefDto>>> GetAttributeDefs()
    {
        var attributes = await _context.AttributeDefs
            .Select(a => new AttributeDefDto
            {
                AttributeId = a.AttributeId,
                AttributeCode = a.AttributeCode,
                AttributeName = a.AttributeName,
                DataType = a.DataType,
                UnitDefault = a.UnitDefault,
                Description = a.Description,
                IsActive = a.IsActive,
                CreatedAt = a.CreatedAt,
                CreatedBy = a.CreatedBy,
                ModifiedAt = a.ModifiedAt,
                ModifiedBy = a.ModifiedBy
            })
            .ToListAsync();

        return Ok(attributes);
    }

    // GET: api/AttributeDef/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AttributeDefDto>> GetAttributeDef(int id)
    {
        var attribute = await _context.AttributeDefs
            .Where(a => a.AttributeId == id)
            .Select(a => new AttributeDefDto
            {
                AttributeId = a.AttributeId,
                AttributeCode = a.AttributeCode,
                AttributeName = a.AttributeName,
                DataType = a.DataType,
                UnitDefault = a.UnitDefault,
                Description = a.Description,
                IsActive = a.IsActive,
                CreatedAt = a.CreatedAt,
                CreatedBy = a.CreatedBy,
                ModifiedAt = a.ModifiedAt,
                ModifiedBy = a.ModifiedBy
            })
            .FirstOrDefaultAsync();

        if (attribute == null)
        {
            return NotFound();
        }

        return Ok(attribute);
    }

    // POST: api/AttributeDef
    [HttpPost]
    public async Task<ActionResult<AttributeDefDto>> CreateAttributeDef(CreateAttributeDefDto dto)
    {
        var attribute = new AttributeDef
        {
            AttributeCode = dto.AttributeCode,
            AttributeName = dto.AttributeName,
            DataType = dto.DataType,
            UnitDefault = dto.UnitDefault,
            Description = dto.Description,
            IsActive = dto.IsActive
        };

        _context.AttributeDefs.Add(attribute);
        await _context.SaveChangesAsync();

        var resultDto = new AttributeDefDto
        {
            AttributeId = attribute.AttributeId,
            AttributeCode = attribute.AttributeCode,
            AttributeName = attribute.AttributeName,
            DataType = attribute.DataType,
            UnitDefault = attribute.UnitDefault,
            Description = attribute.Description,
            IsActive = attribute.IsActive,
            CreatedAt = attribute.CreatedAt,
            CreatedBy = attribute.CreatedBy,
            ModifiedAt = attribute.ModifiedAt,
            ModifiedBy = attribute.ModifiedBy
        };

        return CreatedAtAction(nameof(GetAttributeDef), new { id = attribute.AttributeId }, resultDto);
    }

    // PUT: api/AttributeDef/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAttributeDef(int id, UpdateAttributeDefDto dto)
    {
        var attribute = await _context.AttributeDefs.FindAsync(id);

        if (attribute == null)
        {
            return NotFound();
        }

        attribute.AttributeCode = dto.AttributeCode;
        attribute.AttributeName = dto.AttributeName;
        attribute.DataType = dto.DataType;
        attribute.UnitDefault = dto.UnitDefault;
        attribute.Description = dto.Description;
        attribute.IsActive = dto.IsActive;
        attribute.ModifiedAt = DateTime.UtcNow;
        attribute.ModifiedBy = "System"; // TODO: Get from authenticated user

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/AttributeDef/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttributeDef(int id)
    {
        var attribute = await _context.AttributeDefs.FindAsync(id);

        if (attribute == null)
        {
            return NotFound();
        }

        _context.AttributeDefs.Remove(attribute);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
