using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services;

namespace Parason_Api.Controllers
{    
    public class ModelsController : BaseController
    {
        private readonly IModelService _service;

        public ModelsController(IModelService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<ModelDto>>> GetAll([FromQuery] PaginationParams paginationParams)
        {
            try
            {
                var result = await _service.GetAllAsync(paginationParams);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving models", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModelDto>> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                    return NotFound(new { message = $"Model with ID {id} not found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving model", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ModelDto>> Create([FromBody] CreateModelDto dto)
        {
            try
            {
                var createdBy = User.Identity?.Name ?? "System";
                var result = await _service.CreateAsync(dto, createdBy);
                return CreatedAtAction(nameof(GetById), new { id = result.ModelID }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating model", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ModelDto>> Update(int id, [FromBody] UpdateModelDto dto)
        {
            try
            {
                var modifiedBy = User.Identity?.Name ?? "System";
                var result = await _service.UpdateAsync(id, dto, modifiedBy);
                if (result == null)
                    return NotFound(new { message = $"Model with ID {id} not found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating model", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = $"Model with ID {id} not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting model", error = ex.Message });
            }
        }

        [HttpGet("byseries/{seriesId}")]
        public async Task<ActionResult<List<ModelDto>>> GetBySeries(int seriesId)
        {
            try
            {
                var result = await _service.GetModelsBySeriesAsync(seriesId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving models", error = ex.Message });
            }
        }
    }
}
