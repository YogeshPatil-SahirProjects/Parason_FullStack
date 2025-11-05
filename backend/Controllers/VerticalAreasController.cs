using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services;

namespace Parason_Api.Controllers
{
    public class VerticalAreasController : BaseController
    {
        private readonly IVerticalAreaService _service;
        private readonly ICatalogService _catalogService;

        public VerticalAreasController(IVerticalAreaService service, ICatalogService catalogService)
        {
            _service = service;
            _catalogService = catalogService;
        }

        [HttpGet("Verticals")]
        public async Task<ActionResult<List<VerticalAreaDto>>> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving vertical areas", error = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VerticalAreaDto>> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                    return NotFound(new { message = $"Vertical area with ID {id} not found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving vertical area", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<VerticalAreaDto>> Create([FromBody] CreateVerticalAreaDto dto)
        {
            try
            {
                var createdBy = User.Identity?.Name ?? "System";
                var result = await _service.CreateAsync(dto, createdBy);
                return CreatedAtAction(nameof(GetById), new { id = result.VerticalID }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating vertical area", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VerticalAreaDto>> Update(int id, [FromBody] UpdateVerticalAreaDto dto)
        {
            try
            {
                var modifiedBy = User.Identity?.Name ?? "System";
                var result = await _service.UpdateAsync(id, dto, modifiedBy);
                if (result == null)
                    return NotFound(new { message = $"Vertical area with ID {id} not found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating vertical area", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = $"Vertical area with ID {id} not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting vertical area", error = ex.Message });
            }
        }

        [HttpGet("catalog")]
        public async Task<ActionResult<List<CatalogVerticalDto>>> GetCatalog()
        {
            try
            {
                var result = await _service.GetVerticalCatalogAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving catalog", error = ex.Message });
            }
        }

        [HttpGet("{id:int}/Processes")]
        public async Task<ActionResult<List<ProcessDto>>> GetProcessesBasedOnVerticalId(int id)
        {
            try
            {
                var result = await _service.GetProcessForSelectedVerticalAsync(id);

                if (result == null)
                    return NotFound(new { message = $"Vertical with ID {id} not found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving catalog", error = ex.Message });
            }
        }

    }
}
