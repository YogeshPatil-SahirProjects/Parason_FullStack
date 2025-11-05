using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VerticalAreaController : ControllerBase
{
    private readonly IVerticalAreaService _verticalAreaService;

    public VerticalAreaController(IVerticalAreaService verticalAreaService)
    {
        _verticalAreaService = verticalAreaService;
    }

    // GET: api/VerticalArea
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VerticalAreaDto>>> GetVerticalAreas()
    {
        var verticals = await _verticalAreaService.GetAllAsync();
        return Ok(verticals);
    }

    // GET: api/VerticalArea/5
    [HttpGet("{id}")]
    public async Task<ActionResult<VerticalAreaDto>> GetVerticalArea(int id)
    {
        var vertical = await _verticalAreaService.GetByIdAsync(id);

        if (vertical == null)
        {
            return NotFound();
        }

        return Ok(vertical);
    }

    // POST: api/VerticalArea
    [HttpPost]
    public async Task<ActionResult<VerticalAreaDto>> CreateVerticalArea(CreateVerticalAreaDto dto)
    {
        var vertical = await _verticalAreaService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetVerticalArea), new { id = vertical.VerticalId }, vertical);
    }

    // PUT: api/VerticalArea/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVerticalArea(int id, UpdateVerticalAreaDto dto)
    {
        var success = await _verticalAreaService.UpdateAsync(id, dto);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/VerticalArea/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVerticalArea(int id)
    {
        var success = await _verticalAreaService.DeleteAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
