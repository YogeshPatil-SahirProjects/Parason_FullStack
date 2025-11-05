using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services;

namespace Parason_Api.Controllers
{    
    public class SeriesController : BaseController
    {
        private readonly ISeriesService _service;

        public SeriesController(ISeriesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<SeriesDto>>> GetAll([FromQuery] PaginationParams paginationParams)
        {
            try
            {
                var result = await _service.GetAllAsync(paginationParams);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving series", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SeriesDto>> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                    return NotFound(new { message = $"Series with ID {id} not found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving series", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<SeriesDto>> Create([FromBody] CreateSeriesDto dto)
        {
            try
            {
                var createdBy = User.Identity?.Name ?? "System";
                var result = await _service.CreateAsync(dto, createdBy);
                return CreatedAtAction(nameof(GetById), new { id = result.SeriesID }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating series", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SeriesDto>> Update(int id, [FromBody] UpdateSeriesDto dto)
        {
            try
            {
                var modifiedBy = User.Identity?.Name ?? "System";
                var result = await _service.UpdateAsync(id, dto, modifiedBy);
                if (result == null)
                    return NotFound(new { message = $"Series with ID {id} not found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating series", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = $"Series with ID {id} not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting series", error = ex.Message });
            }
        }

        [HttpGet("byequipment/{equipmentId}")]
        public async Task<ActionResult<List<SeriesDto>>> GetByEquipment(int equipmentId)
        {
            try
            {
                var result = await _service.GetSeriesByEquipmentAsync(equipmentId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving series", error = ex.Message });
            }
        }

        [HttpGet("Models/{seriesId}")]
        public async Task<ActionResult<List<ModelDto>>> GetModelsBySeriesId(int seriesId)
        {
            try
            {
                var result = await _service.GetModelsBySeriesId(seriesId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving series", error = ex.Message });
            }
        }
    }
}
