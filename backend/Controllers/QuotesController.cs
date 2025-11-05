using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services;

namespace Parason_Api.Controllers
{
    public class QuotesController : BaseController
    {
        private readonly IQuoteService _service;

        public QuotesController(IQuoteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<QuoteHeaderDto>>> GetAll([FromQuery] PaginationParams paginationParams)
        {
            try
            {
                var result = await _service.GetAllAsync(paginationParams);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving quotes", error = ex.Message });
            }
        }

        [HttpGet("{quoteId}/{revision}")]
        public async Task<ActionResult<QuoteHeaderDto>> GetById(int quoteId, byte revision)
        {
            try
            {
                var result = await _service.GetByIdAsync(quoteId, revision);
                if (result == null)
                    return NotFound(new { message = $"Quote with ID {quoteId} revision {revision} not found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving quote", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<QuoteHeaderDto>> Create([FromBody] CreateQuoteHeaderDto dto)
        {
            try
            {
                var createdBy = User.Identity?.Name ?? "System";
                var result = await _service.CreateAsync(dto, createdBy);
                return CreatedAtAction(nameof(GetById), new { quoteId = result.QuoteID, revision = result.QuoteRevision }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating quote", error = ex.Message });
            }
        }

        [HttpPut("{quoteId}/{revision}")]
        public async Task<ActionResult<QuoteHeaderDto>> Update(int quoteId, byte revision, [FromBody] UpdateQuoteHeaderDto dto)
        {
            try
            {
                var modifiedBy = User.Identity?.Name ?? "System";
                var result = await _service.UpdateAsync(quoteId, revision, dto, modifiedBy);
                if (result == null)
                    return NotFound(new { message = $"Quote with ID {quoteId} revision {revision} not found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating quote", error = ex.Message });
            }
        }

        [HttpDelete("{quoteId}/{revision}")]
        public async Task<ActionResult> Delete(int quoteId, byte revision)
        {
            try
            {
                var result = await _service.DeleteAsync(quoteId, revision);
                if (!result)
                    return NotFound(new { message = $"Quote with ID {quoteId} revision {revision} not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting quote", error = ex.Message });
            }
        }

        [HttpPost("{quoteId}/{revision}/createrevision")]
        public async Task<ActionResult<QuoteHeaderDto>> CreateRevision(int quoteId, byte revision)
        {
            try
            {
                var createdBy = User.Identity?.Name ?? "System";
                var result = await _service.CreateRevisionAsync(quoteId, revision, createdBy);
                if (result == null)
                    return NotFound(new { message = $"Quote with ID {quoteId} revision {revision} not found" });

                return CreatedAtAction(nameof(GetById), new { quoteId = result.QuoteID, revision = result.QuoteRevision }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating quote revision", error = ex.Message });
            }
        }

        [HttpGet("bycustomer/{customerName}")]
        public async Task<ActionResult<List<QuoteHeaderDto>>> GetByCustomer(string customerName)
        {
            try
            {
                var result = await _service.GetQuotesByCustomerAsync(customerName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving quotes", error = ex.Message });
            }
        }

        [HttpGet("bystatus/{status}")]
        public async Task<ActionResult<List<QuoteHeaderDto>>> GetByStatus(string status)
        {
            try
            {
                var result = await _service.GetQuotesByStatusAsync(status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving quotes", error = ex.Message });
            }
        }
    }
}
