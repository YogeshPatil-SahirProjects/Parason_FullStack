using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parason_Api.Models;
using Parason_Api.DTOs;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuoteVerticalController : ControllerBase
{
    private readonly ParasonDbContext _context;

    public QuoteVerticalController(ParasonDbContext context)
    {
        _context = context;
    }

    // GET: api/QuoteVertical
    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuoteVerticalDto>>> GetQuoteVerticals()
    {
        var quoteVerticals = await _context.QuoteVerticals
            .Include(q => q.Vertical)
            .Include(q => q.Process)
            .Select(q => new QuoteVerticalDto
            {
                RecordId = q.RecordId,
                QuoteId = q.QuoteId,
                QuoteRevision = q.QuoteRevision,
                Layer = q.Layer,
                VerticalId = q.VerticalId,
                ProcessId = q.ProcessId,
                CreatedAt = q.CreatedAt,
                CreatedBy = q.CreatedBy,
                VerticalName = q.Vertical.VerticalName,
                ProcessName = q.Process.ProcessName
            })
            .ToListAsync();

        return Ok(quoteVerticals);
    }

    // GET: api/QuoteVertical/5
    [HttpGet("{id}")]
    public async Task<ActionResult<QuoteVerticalDto>> GetQuoteVertical(int id)
    {
        var quoteVertical = await _context.QuoteVerticals
            .Include(q => q.Vertical)
            .Include(q => q.Process)
            .Where(q => q.RecordId == id)
            .Select(q => new QuoteVerticalDto
            {
                RecordId = q.RecordId,
                QuoteId = q.QuoteId,
                QuoteRevision = q.QuoteRevision,
                Layer = q.Layer,
                VerticalId = q.VerticalId,
                ProcessId = q.ProcessId,
                CreatedAt = q.CreatedAt,
                CreatedBy = q.CreatedBy,
                VerticalName = q.Vertical.VerticalName,
                ProcessName = q.Process.ProcessName
            })
            .FirstOrDefaultAsync();

        if (quoteVertical == null)
        {
            return NotFound();
        }

        return Ok(quoteVertical);
    }

    // GET: api/QuoteVertical/ByQuote/5/0
    [HttpGet("ByQuote/{quoteId}/{revision}")]
    public async Task<ActionResult<IEnumerable<QuoteVerticalDto>>> GetQuoteVerticalsByQuote(int quoteId, byte revision)
    {
        var quoteVerticals = await _context.QuoteVerticals
            .Where(q => q.QuoteId == quoteId && q.QuoteRevision == revision)
            .Include(q => q.Vertical)
            .Include(q => q.Process)
            .Select(q => new QuoteVerticalDto
            {
                RecordId = q.RecordId,
                QuoteId = q.QuoteId,
                QuoteRevision = q.QuoteRevision,
                Layer = q.Layer,
                VerticalId = q.VerticalId,
                ProcessId = q.ProcessId,
                CreatedAt = q.CreatedAt,
                CreatedBy = q.CreatedBy,
                VerticalName = q.Vertical.VerticalName,
                ProcessName = q.Process.ProcessName
            })
            .ToListAsync();

        return Ok(quoteVerticals);
    }

    // POST: api/QuoteVertical
    [HttpPost]
    public async Task<ActionResult<QuoteVerticalDto>> CreateQuoteVertical(CreateQuoteVerticalDto dto)
    {
        var quoteVertical = new QuoteVertical
        {
            QuoteId = dto.QuoteId,
            QuoteRevision = dto.QuoteRevision,
            Layer = dto.Layer,
            VerticalId = dto.VerticalId,
            ProcessId = dto.ProcessId
        };

        _context.QuoteVerticals.Add(quoteVertical);
        await _context.SaveChangesAsync();

        var vertical = await _context.VerticalAreas.FindAsync(dto.VerticalId);
        var process = await _context.Processes.FindAsync(dto.ProcessId);

        var resultDto = new QuoteVerticalDto
        {
            RecordId = quoteVertical.RecordId,
            QuoteId = quoteVertical.QuoteId,
            QuoteRevision = quoteVertical.QuoteRevision,
            Layer = quoteVertical.Layer,
            VerticalId = quoteVertical.VerticalId,
            ProcessId = quoteVertical.ProcessId,
            CreatedAt = quoteVertical.CreatedAt,
            CreatedBy = quoteVertical.CreatedBy,
            VerticalName = vertical?.VerticalName,
            ProcessName = process?.ProcessName
        };

        return CreatedAtAction(nameof(GetQuoteVertical), new { id = quoteVertical.RecordId }, resultDto);
    }

    // PUT: api/QuoteVertical/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuoteVertical(int id, UpdateQuoteVerticalDto dto)
    {
        var quoteVertical = await _context.QuoteVerticals.FindAsync(id);

        if (quoteVertical == null)
        {
            return NotFound();
        }

        quoteVertical.QuoteId = dto.QuoteId;
        quoteVertical.QuoteRevision = dto.QuoteRevision;
        quoteVertical.Layer = dto.Layer;
        quoteVertical.VerticalId = dto.VerticalId;
        quoteVertical.ProcessId = dto.ProcessId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/QuoteVertical/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuoteVertical(int id)
    {
        var quoteVertical = await _context.QuoteVerticals.FindAsync(id);

        if (quoteVertical == null)
        {
            return NotFound();
        }

        _context.QuoteVerticals.Remove(quoteVertical);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
