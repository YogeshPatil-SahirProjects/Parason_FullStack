using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttributeDefController : ControllerBase
{
    private readonly IAttributeDefService _attributeDefService;

    public AttributeDefController(IAttributeDefService attributeDefService)
    {
        _attributeDefService = attributeDefService;
    }

    // GET: api/AttributeDef
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AttributeDefDto>>> GetAttributeDefs()
    {
        var attributes = await _attributeDefService.GetAllAsync();
        return Ok(attributes);
    }

    // GET: api/AttributeDef/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AttributeDefDto>> GetAttributeDef(int id)
    {
        var attribute = await _attributeDefService.GetByIdAsync(id);

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
        var attribute = await _attributeDefService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAttributeDef), new { id = attribute.AttributeId }, attribute);
    }

    // PUT: api/AttributeDef/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAttributeDef(int id, UpdateAttributeDefDto dto)
    {
        var success = await _attributeDefService.UpdateAsync(id, dto);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/AttributeDef/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttributeDef(int id)
    {
        var success = await _attributeDefService.DeleteAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
