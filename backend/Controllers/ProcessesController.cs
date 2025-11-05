using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services;

namespace Parason_Api.Controllers
{    
    public class ProcessesController : BaseController
    {
        private readonly IProcessService _service;

        public ProcessesController(IProcessService service)
        {
            _service = service;
        }

        [HttpGet("AllProcesses")]
        public async Task<ActionResult<List<ProcessDto>>> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving processes", error = ex.Message });
            }
        }

        [HttpGet("{id}", Name = "GetProcessById")]
        public async Task<ActionResult<ProcessDto>> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                    return NotFound(new { message = $"Process with ID {id} not found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving process", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProcessDto>> Create([FromBody] CreateProcessDto dto)
        {
            try
            {
                var createdBy = User.Identity?.Name ?? "System";
                var result = await _service.CreateAsync(dto, createdBy);
                return CreatedAtAction(nameof(GetById), new { id = result.ProcessID }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating process", error = ex.Message });
            }
        }

        [HttpPut("{id}", Name = "UpdateProcess")]
        public async Task<ActionResult<ProcessDto>> Update(int id, [FromBody] UpdateProcessDto dto)
        {
            try
            {
                var modifiedBy = User.Identity?.Name ?? "System";
                var result = await _service.UpdateAsync(id, dto, modifiedBy);
                if (result == null)
                    return NotFound(new { message = $"Process with ID {id} not found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating process", error = ex.Message });
            }
        }

        [HttpDelete("{id}", Name = "DeleteProcess")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                if (!result)
                    return NotFound(new { message = $"Process with ID {id} not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting process", error = ex.Message });
            }
        }

        [HttpGet("byvertical/{verticalId}")]
        public async Task<ActionResult<List<ProcessDto>>> GetByVertical(int verticalId)
        {
            try
            {
                var result = await _service.GetProcessesByVerticalAsync(verticalId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving processes", error = ex.Message });
            }
        }

        [HttpGet("{id:int}/Equipments")]
        public async Task<ActionResult<List<EquipmentDto>>> GetProcessesBasedOnVerticalId(int id)
        {
            try
            {
                var result = await _service.GetEquipmentsByProcessAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving processes", error = ex.Message }); ;
            }
        }
    }
}
