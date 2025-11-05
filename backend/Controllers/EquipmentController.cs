using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly IEquipmentService _equipmentService;

    public EquipmentController(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }

    // GET: api/Equipment
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetEquipment()
    {
        var equipment = await _equipmentService.GetAllAsync();
        return Ok(equipment);
    }

    // GET: api/Equipment/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EquipmentDto>> GetEquipment(int id)
    {
        var equipment = await _equipmentService.GetByIdAsync(id);

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
        var equipment = await _equipmentService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetEquipment), new { id = equipment.EquipmentId }, equipment);
    }

    // PUT: api/Equipment/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEquipment(int id, UpdateEquipmentDto dto)
    {
        var success = await _equipmentService.UpdateAsync(id, dto);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/Equipment/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEquipment(int id)
    {
        var success = await _equipmentService.DeleteAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
