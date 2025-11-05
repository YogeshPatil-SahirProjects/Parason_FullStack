using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuoteHeaderController : ControllerBase
{
    private readonly IQuoteHeaderService _quoteHeaderService;

    public QuoteHeaderController(IQuoteHeaderService quoteHeaderService)
    {
        _quoteHeaderService = quoteHeaderService;
    }

    // GET: api/QuoteHeader
    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuoteHeaderDto>>> GetQuoteHeaders()
    {
        var quotes = await _quoteHeaderService.GetAllAsync();
        return Ok(quotes);
    }

    // GET: api/QuoteHeader/search
    [HttpPost("search")]
    public async Task<ActionResult<PagedResult<QuoteHeaderDto>>> SearchQuotes(QuoteSearchDto searchDto)
    {
        var result = await _quoteHeaderService.GetPagedAsync(searchDto);
        return Ok(result);
    }

    // GET: api/QuoteHeader/5/0
    [HttpGet("{quoteId}/{revision}")]
    public async Task<ActionResult<QuoteHeaderDto>> GetQuoteHeader(int quoteId, byte revision)
    {
        var quote = await _quoteHeaderService.GetByIdAsync(quoteId, revision);

        if (quote == null)
        {
            return NotFound();
        }

        return Ok(quote);
    }

    // POST: api/QuoteHeader
    [HttpPost]
    public async Task<ActionResult<QuoteHeaderDto>> CreateQuoteHeader(CreateQuoteHeaderDto dto)
    {
        var quote = await _quoteHeaderService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetQuoteHeader), new { quoteId = quote.QuoteId, revision = quote.QuoteRevision }, quote);
    }

    // PUT: api/QuoteHeader/5/0
    [HttpPut("{quoteId}/{revision}")]
    public async Task<IActionResult> UpdateQuoteHeader(int quoteId, byte revision, UpdateQuoteHeaderDto dto)
    {
        var success = await _quoteHeaderService.UpdateAsync(quoteId, revision, dto);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/QuoteHeader/5/0
    [HttpDelete("{quoteId}/{revision}")]
    public async Task<IActionResult> DeleteQuoteHeader(int quoteId, byte revision)
    {
        var success = await _quoteHeaderService.DeleteAsync(quoteId, revision);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
