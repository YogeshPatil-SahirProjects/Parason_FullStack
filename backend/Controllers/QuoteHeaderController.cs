using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parason_Api.Models;
using Parason_Api.DTOs;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuoteHeaderController : ControllerBase
{
    private readonly ParasonDbContext _context;

    public QuoteHeaderController(ParasonDbContext context)
    {
        _context = context;
    }

    // GET: api/QuoteHeader
    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuoteHeaderDto>>> GetQuoteHeaders()
    {
        var quotes = await _context.QuoteHeaders
            .Select(q => new QuoteHeaderDto
            {
                QuoteId = q.QuoteId,
                QuoteRevision = q.QuoteRevision,
                QuoteNumber = q.QuoteNumber,
                QuoteName = q.QuoteName,
                CustomerName = q.CustomerName,
                Status = q.Status,
                Currency = q.Currency,
                ValidityDays = q.ValidityDays,
                Notes = q.Notes,
                CreatedAt = q.CreatedAt,
                CreatedBy = q.CreatedBy,
                ModifiedAt = q.ModifiedAt,
                ModifiedBy = q.ModifiedBy
            })
            .ToListAsync();

        return Ok(quotes);
    }

    // GET: api/QuoteHeader/5/0
    [HttpGet("{quoteId}/{revision}")]
    public async Task<ActionResult<QuoteHeaderDto>> GetQuoteHeader(int quoteId, byte revision)
    {
        var quote = await _context.QuoteHeaders
            .Where(q => q.QuoteId == quoteId && q.QuoteRevision == revision)
            .Select(q => new QuoteHeaderDto
            {
                QuoteId = q.QuoteId,
                QuoteRevision = q.QuoteRevision,
                QuoteNumber = q.QuoteNumber,
                QuoteName = q.QuoteName,
                CustomerName = q.CustomerName,
                Status = q.Status,
                Currency = q.Currency,
                ValidityDays = q.ValidityDays,
                Notes = q.Notes,
                CreatedAt = q.CreatedAt,
                CreatedBy = q.CreatedBy,
                ModifiedAt = q.ModifiedAt,
                ModifiedBy = q.ModifiedBy
            })
            .FirstOrDefaultAsync();

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
        var quote = new QuoteHeader
        {
            QuoteRevision = dto.QuoteRevision,
            QuoteNumber = dto.QuoteNumber,
            QuoteName = dto.QuoteName,
            CustomerName = dto.CustomerName,
            Status = dto.Status,
            Currency = dto.Currency,
            ValidityDays = dto.ValidityDays,
            Notes = dto.Notes
        };

        _context.QuoteHeaders.Add(quote);
        await _context.SaveChangesAsync();

        var resultDto = new QuoteHeaderDto
        {
            QuoteId = quote.QuoteId,
            QuoteRevision = quote.QuoteRevision,
            QuoteNumber = quote.QuoteNumber,
            QuoteName = quote.QuoteName,
            CustomerName = quote.CustomerName,
            Status = quote.Status,
            Currency = quote.Currency,
            ValidityDays = quote.ValidityDays,
            Notes = quote.Notes,
            CreatedAt = quote.CreatedAt,
            CreatedBy = quote.CreatedBy,
            ModifiedAt = quote.ModifiedAt,
            ModifiedBy = quote.ModifiedBy
        };

        return CreatedAtAction(nameof(GetQuoteHeader), new { quoteId = quote.QuoteId, revision = quote.QuoteRevision }, resultDto);
    }

    // PUT: api/QuoteHeader/5/0
    [HttpPut("{quoteId}/{revision}")]
    public async Task<IActionResult> UpdateQuoteHeader(int quoteId, byte revision, UpdateQuoteHeaderDto dto)
    {
        var quote = await _context.QuoteHeaders.FindAsync(quoteId, revision);

        if (quote == null)
        {
            return NotFound();
        }

        quote.QuoteRevision = dto.QuoteRevision;
        quote.QuoteNumber = dto.QuoteNumber;
        quote.QuoteName = dto.QuoteName;
        quote.CustomerName = dto.CustomerName;
        quote.Status = dto.Status;
        quote.Currency = dto.Currency;
        quote.ValidityDays = dto.ValidityDays;
        quote.Notes = dto.Notes;
        quote.ModifiedAt = DateTime.UtcNow;
        quote.ModifiedBy = "System"; // TODO: Get from authenticated user

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/QuoteHeader/5/0
    [HttpDelete("{quoteId}/{revision}")]
    public async Task<IActionResult> DeleteQuoteHeader(int quoteId, byte revision)
    {
        var quote = await _context.QuoteHeaders.FindAsync(quoteId, revision);

        if (quote == null)
        {
            return NotFound();
        }

        _context.QuoteHeaders.Remove(quote);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
