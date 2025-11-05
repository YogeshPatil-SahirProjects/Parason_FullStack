using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttributeListValueController : ControllerBase
{
    private readonly IAttributeListValueService _attributeListValueService;

    public AttributeListValueController(IAttributeListValueService attributeListValueService)
    {
        _attributeListValueService = attributeListValueService;
    }

    // GET: api/AttributeListValue
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AttributeListValueDto>>> GetAttributeListValues()
    {
        var listValues = await _attributeListValueService.GetAllAsync();
        return Ok(listValues);
    }

    // GET: api/AttributeListValue/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AttributeListValueDto>> GetAttributeListValue(int id)
    {
        var listValue = await _attributeListValueService.GetByIdAsync(id);

        if (listValue == null)
        {
            return NotFound();
        }

        return Ok(listValue);
    }

    // POST: api/AttributeListValue
    [HttpPost]
    public async Task<ActionResult<AttributeListValueDto>> CreateAttributeListValue(CreateAttributeListValueDto dto)
    {
        var listValue = await _attributeListValueService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAttributeListValue), new { id = listValue.ListValueId }, listValue);
    }

    // PUT: api/AttributeListValue/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAttributeListValue(int id, UpdateAttributeListValueDto dto)
    {
        var success = await _attributeListValueService.UpdateAsync(id, dto);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/AttributeListValue/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttributeListValue(int id)
    {
        var success = await _attributeListValueService.DeleteAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
