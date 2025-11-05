using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services;

namespace Parason_Api.Controllers
{
    public class EquipmentsController : BaseController
    {
        private readonly IEquipmentService _service;

        public EquipmentsController(IEquipmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<EquipmentDto>>> GetAll([FromQuery] PaginationParams paginationParams)
        {
            try
            {
                var result = await _service.GetAllAsync(paginationParams);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving equipments", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentDto>> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                    return NotFound(new { message = $"Equipment with ID {id} not found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving equipment", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentDto>> Create([FromBody] CreateEquipmentDto dto)
        {
            try
            {
                var createdBy = User.Identity?.Name ?? "System";
                var result = await _service.CreateAsync(dto, createdBy);
                return CreatedAtAction(nameof(GetById), new { id = result.EquipmentID }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating equipment", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EquipmentDto>> Update(int id, [FromBody] UpdateEquipmentDto dto)
        {
            try
            {
                var modifiedBy = User.Identity?.Name ?? "System";
                var result = await _service.UpdateAsync(id, dto, modifiedBy);
                if (result == null)
                    return NotFound(new { message = $"Equipment with ID {id} not found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating equipment", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = $"Equipment with ID {id} not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting equipment", error = ex.Message });
            }
        }

        [HttpGet("byprocess/{processId}")]
        public async Task<ActionResult<List<EquipmentDto>>> GetByProcess(int processId)
        {
            try
            {
                var result = await _service.GetEquipmentsByProcessAsync(processId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving equipments", error = ex.Message });
            }
        }

        [HttpGet("Series/{equipmentId}")]
        public async Task<ActionResult<List<SeriesDto>>> GetSeriesByEquipmentId(int equipmentId)
        {
            try
            {
                var result = await _service.GetSeriesByEquipmentId(equipmentId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving equipments", error = ex.Message });
            }
        }

        [HttpGet("EquipmentAttributes/{equipmentId}")]
        public async Task<ActionResult<List<EquipmentAttributeDto>>> GetEquipmentAttributes(int equipmentId)
        {
            var attributes = await _service.GetEquipmentAttributes(equipmentId);
            if (attributes == null || !attributes.Any())
                return NotFound("No attributes found for this equipment.");

            return Ok(attributes);
        }


    }
}
