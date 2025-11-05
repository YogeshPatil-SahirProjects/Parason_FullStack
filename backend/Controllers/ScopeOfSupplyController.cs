using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parason_Api.Models;
using Parason_Api.DTOs;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScopeOfSupplyController : ControllerBase
{
    private readonly ParasonDbContext _context;

    public ScopeOfSupplyController(ParasonDbContext context)
    {
        _context = context;
    }

    // GET: api/ScopeOfSupply
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScopeOfSupplyDto>>> GetScopeOfSupplies()
    {
        var items = await _context.ScopeOfSupplies
            .Include(s => s.Item)
            .Include(s => s.Model)
            .Select(s => new ScopeOfSupplyDto
            {
                RecordId = s.RecordId,
                ModelId = s.ModelId,
                ItemId = s.ItemId,
                PriceInr = s.PriceInr,
                Quantity = s.Quantity,
                ItemName = s.Item.ItemName,
                ModelName = s.Model != null ? s.Model.ModelName : null
            })
            .ToListAsync();

        return Ok(items);
    }

    // GET: api/ScopeOfSupply/5/10
    [HttpGet("{recordId}/{itemId}")]
    public async Task<ActionResult<ScopeOfSupplyDto>> GetScopeOfSupply(int recordId, int itemId)
    {
        var item = await _context.ScopeOfSupplies
            .Include(s => s.Item)
            .Include(s => s.Model)
            .Where(s => s.RecordId == recordId && s.ItemId == itemId)
            .Select(s => new ScopeOfSupplyDto
            {
                RecordId = s.RecordId,
                ModelId = s.ModelId,
                ItemId = s.ItemId,
                PriceInr = s.PriceInr,
                Quantity = s.Quantity,
                ItemName = s.Item.ItemName,
                ModelName = s.Model != null ? s.Model.ModelName : null
            })
            .FirstOrDefaultAsync();

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    // GET: api/ScopeOfSupply/ByRecord/5
    [HttpGet("ByRecord/{recordId}")]
    public async Task<ActionResult<IEnumerable<ScopeOfSupplyDto>>> GetScopeOfSuppliesByRecord(int recordId)
    {
        var items = await _context.ScopeOfSupplies
            .Where(s => s.RecordId == recordId)
            .Include(s => s.Item)
            .Include(s => s.Model)
            .Select(s => new ScopeOfSupplyDto
            {
                RecordId = s.RecordId,
                ModelId = s.ModelId,
                ItemId = s.ItemId,
                PriceInr = s.PriceInr,
                Quantity = s.Quantity,
                ItemName = s.Item.ItemName,
                ModelName = s.Model != null ? s.Model.ModelName : null
            })
            .ToListAsync();

        return Ok(items);
    }

    // POST: api/ScopeOfSupply
    [HttpPost]
    public async Task<ActionResult<ScopeOfSupplyDto>> CreateScopeOfSupply(CreateScopeOfSupplyDto dto)
    {
        var item = new ScopeOfSupply
        {
            RecordId = dto.RecordId,
            ModelId = dto.ModelId,
            ItemId = dto.ItemId,
            PriceInr = dto.PriceInr,
            Quantity = dto.Quantity
        };

        _context.ScopeOfSupplies.Add(item);
        await _context.SaveChangesAsync();

        var itemMaster = await _context.ItemMasters.FindAsync(dto.ItemId);
        var model = dto.ModelId.HasValue ? await _context.Models.FindAsync(dto.ModelId) : null;

        var resultDto = new ScopeOfSupplyDto
        {
            RecordId = item.RecordId,
            ModelId = item.ModelId,
            ItemId = item.ItemId,
            PriceInr = item.PriceInr,
            Quantity = item.Quantity,
            ItemName = itemMaster?.ItemName,
            ModelName = model?.ModelName
        };

        return CreatedAtAction(nameof(GetScopeOfSupply), new { recordId = item.RecordId, itemId = item.ItemId }, resultDto);
    }

    // PUT: api/ScopeOfSupply/5/10
    [HttpPut("{recordId}/{itemId}")]
    public async Task<IActionResult> UpdateScopeOfSupply(int recordId, int itemId, UpdateScopeOfSupplyDto dto)
    {
        var item = await _context.ScopeOfSupplies.FindAsync(recordId, itemId);

        if (item == null)
        {
            return NotFound();
        }

        item.ModelId = dto.ModelId;
        item.PriceInr = dto.PriceInr;
        item.Quantity = dto.Quantity;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/ScopeOfSupply/5/10
    [HttpDelete("{recordId}/{itemId}")]
    public async Task<IActionResult> DeleteScopeOfSupply(int recordId, int itemId)
    {
        var item = await _context.ScopeOfSupplies.FindAsync(recordId, itemId);

        if (item == null)
        {
            return NotFound();
        }

        _context.ScopeOfSupplies.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
