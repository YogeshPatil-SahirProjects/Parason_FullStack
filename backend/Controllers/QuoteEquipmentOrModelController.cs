using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services;

namespace Parason_Api.Controllers
{ 
    public class QuoteEquipmentOrModelController : BaseController
    {
        private readonly IQuoteEquipmentOrModelService _service;
        private readonly ILogger<QuoteEquipmentOrModelController> _logger;

        public QuoteEquipmentOrModelController(IQuoteEquipmentOrModelService service, ILogger<QuoteEquipmentOrModelController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("record/{recordId}")]
        public async Task<ActionResult<List<QuoteEquipmentOrModelDto>>> GetByRecord(int recordId)
        {
            try
            {
                var result = await _service.GetByRecordIdAsync(recordId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByRecord");
                return StatusCode(500, "An error occurred while retrieving equipment/models.");
            }
        }

        [HttpGet("{qeomId}")]
        public async Task<ActionResult<QuoteEquipmentOrModelDto>> GetById(int qeomId)
        {
            try
            {
                var result = await _service.GetByIdAsync(qeomId);
                if (result == null)
                {
                    return NotFound($"Equipment/Model with QEOMId {qeomId} not found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetById");
                return StatusCode(500, "An error occurred while retrieving the equipment/model.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<QuoteEquipmentOrModelDto>> Create([FromBody] CreateQuoteEquipmentOrModelDto dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { qeomId = result.QEOMId }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Create");
                return StatusCode(500, "An error occurred while creating the equipment/model.");
            }
        }         

        [HttpPut("{qeomId}")]
        public async Task<ActionResult<QuoteEquipmentOrModelDto>> Update(
            int qeomId,
            [FromBody] CreateQuoteEquipmentOrModelDto dto)
        {
            try
            {
                var result = await _service.UpdateAsync(qeomId, dto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Update");
                return StatusCode(500, "An error occurred while updating the equipment/model.");
            }
        }

        [HttpDelete("{qeomId}")]
        public async Task<ActionResult> Delete(int qeomId)
        {
            try
            {
                var result = await _service.DeleteAsync(qeomId);
                if (!result)
                {
                    return NotFound($"Equipment/Model with QEOMId {qeomId} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Delete");
                return StatusCode(500, "An error occurred while deleting the equipment/model.");
            }
        }
    }
}
