using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services;

namespace Parason_Api.Controllers
{ 
    public class QuoteVerticalController : BaseController
    {
        private readonly IQuoteVerticalService _service;
        private readonly ILogger<QuoteVerticalController> _logger;

        public QuoteVerticalController(IQuoteVerticalService service, ILogger<QuoteVerticalController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("quote/{quoteId}/{quoteRevision}")]
        public async Task<ActionResult<IEnumerable<QuoteVerticalDto>>> GetByQuote(int quoteId, byte quoteRevision)
        {
            try
            {
                var result = await _service.GetByQuoteIdAsync(quoteId, quoteRevision);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByQuote");
                return StatusCode(500, "An error occurred while retrieving quote verticals.");
            }
        }

        [HttpGet("{recordId}")]
        public async Task<ActionResult<QuoteVerticalDto>> GetById(int recordId)
        {
            try
            {
                var result = await _service.GetByRecordIdAsync(recordId);
                if (result == null)
                {
                    return NotFound($"Quote vertical with RecordID {recordId} not found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetById");
                return StatusCode(500, "An error occurred while retrieving the quote vertical.");
            }
        }

        [HttpGet("VerticalConfig/{quoteId:int}/{quoteRevision}/{verticalId:int}")]
        public async Task<ActionResult<QuoteVerticalDto>> GetVerticalConfiguration(int quoteId, byte quoteRevision, int verticalId)
        {
            try
            {
                var result = await _service.GetVerticalsInfoAsync(quoteId, quoteRevision, verticalId);
                if (result == null)
                {
                    return NotFound($"Quote vertical with QuoteId {quoteId} not found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetById");
                return StatusCode(500, "An error occurred while retrieving the quote vertical.");
            }
        }


        [HttpPost("{quoteID}/{revision}")]
        public async Task<ActionResult<QuoteVerticalDto>> Create(int quoteID, int revision,
                                                                 [FromBody] CreateQuoteVerticalDto dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto, "System");
                return CreatedAtAction(nameof(GetById), new { recordId = result.RecordID }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Create");
                return StatusCode(500, "An error occurred while creating the quote vertical.");
            }
        }

        [HttpPut("{recordId}")]
        public async Task<ActionResult<QuoteVerticalDto>> Update(int recordId, [FromBody] QuoteVerticalDto dto)
        {
            try
            {
                var result = await _service.UpdateAsync(recordId, dto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Update");
                return StatusCode(500, "An error occurred while updating the quote vertical.");
            }
        }

        [HttpDelete("{recordId}")]
        public async Task<ActionResult> Delete(int recordId)
        {
            try
            {
                var result = await _service.DeleteAsync(recordId);
                if (!result)
                {
                    return NotFound($"Quote vertical with RecordID {recordId} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Delete");
                return StatusCode(500, "An error occurred while deleting the quote vertical.");
            }
        }
    }
}
