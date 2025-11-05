using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parason_Api.Models;
using Parason_Api.DTOs;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PriceController : ControllerBase
{
    private readonly ParasonDbContext _context;

    public PriceController(ParasonDbContext context)
    {
        _context = context;
    }

    // GET: api/Price
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PriceDto>>> GetPrices()
    {
        var prices = await _context.Prices
            .Include(p => p.Equipment)
            .Include(p => p.Model)
            .Include(p => p.Item)
            .Select(p => new PriceDto
            {
                PriceId = p.PriceId,
                EquipmentId = p.EquipmentId,
                ModelId = p.ModelId,
                ItemId = p.ItemId,
                BasePriceInr = p.BasePriceInr,
                EffectiveFrom = p.EffectiveFrom,
                CreatedAt = p.CreatedAt,
                CreatedBy = p.CreatedBy,
                EquipmentName = p.Equipment != null ? p.Equipment.EquipmentName : null,
                ModelName = p.Model != null ? p.Model.ModelName : null,
                ItemName = p.Item != null ? p.Item.ItemName : null
            })
            .ToListAsync();

        return Ok(prices);
    }

    // GET: api/Price/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PriceDto>> GetPrice(int id)
    {
        var price = await _context.Prices
            .Include(p => p.Equipment)
            .Include(p => p.Model)
            .Include(p => p.Item)
            .Where(p => p.PriceId == id)
            .Select(p => new PriceDto
            {
                PriceId = p.PriceId,
                EquipmentId = p.EquipmentId,
                ModelId = p.ModelId,
                ItemId = p.ItemId,
                BasePriceInr = p.BasePriceInr,
                EffectiveFrom = p.EffectiveFrom,
                CreatedAt = p.CreatedAt,
                CreatedBy = p.CreatedBy,
                EquipmentName = p.Equipment != null ? p.Equipment.EquipmentName : null,
                ModelName = p.Model != null ? p.Model.ModelName : null,
                ItemName = p.Item != null ? p.Item.ItemName : null
            })
            .FirstOrDefaultAsync();

        if (price == null)
        {
            return NotFound();
        }

        return Ok(price);
    }

    // GET: api/Price/ByEquipment/5
    [HttpGet("ByEquipment/{equipmentId}")]
    public async Task<ActionResult<IEnumerable<PriceDto>>> GetPricesByEquipment(int equipmentId)
    {
        var prices = await _context.Prices
            .Where(p => p.EquipmentId == equipmentId)
            .Include(p => p.Equipment)
            .Select(p => new PriceDto
            {
                PriceId = p.PriceId,
                EquipmentId = p.EquipmentId,
                ModelId = p.ModelId,
                ItemId = p.ItemId,
                BasePriceInr = p.BasePriceInr,
                EffectiveFrom = p.EffectiveFrom,
                CreatedAt = p.CreatedAt,
                CreatedBy = p.CreatedBy,
                EquipmentName = p.Equipment != null ? p.Equipment.EquipmentName : null
            })
            .OrderByDescending(p => p.EffectiveFrom)
            .ToListAsync();

        return Ok(prices);
    }

    // GET: api/Price/ByModel/5
    [HttpGet("ByModel/{modelId}")]
    public async Task<ActionResult<IEnumerable<PriceDto>>> GetPricesByModel(int modelId)
    {
        var prices = await _context.Prices
            .Where(p => p.ModelId == modelId)
            .Include(p => p.Model)
            .Select(p => new PriceDto
            {
                PriceId = p.PriceId,
                EquipmentId = p.EquipmentId,
                ModelId = p.ModelId,
                ItemId = p.ItemId,
                BasePriceInr = p.BasePriceInr,
                EffectiveFrom = p.EffectiveFrom,
                CreatedAt = p.CreatedAt,
                CreatedBy = p.CreatedBy,
                ModelName = p.Model != null ? p.Model.ModelName : null
            })
            .OrderByDescending(p => p.EffectiveFrom)
            .ToListAsync();

        return Ok(prices);
    }

    // GET: api/Price/ByItem/5
    [HttpGet("ByItem/{itemId}")]
    public async Task<ActionResult<IEnumerable<PriceDto>>> GetPricesByItem(int itemId)
    {
        var prices = await _context.Prices
            .Where(p => p.ItemId == itemId)
            .Include(p => p.Item)
            .Select(p => new PriceDto
            {
                PriceId = p.PriceId,
                EquipmentId = p.EquipmentId,
                ModelId = p.ModelId,
                ItemId = p.ItemId,
                BasePriceInr = p.BasePriceInr,
                EffectiveFrom = p.EffectiveFrom,
                CreatedAt = p.CreatedAt,
                CreatedBy = p.CreatedBy,
                ItemName = p.Item != null ? p.Item.ItemName : null
            })
            .OrderByDescending(p => p.EffectiveFrom)
            .ToListAsync();

        return Ok(prices);
    }

    // POST: api/Price
    [HttpPost]
    public async Task<ActionResult<PriceDto>> CreatePrice(CreatePriceDto dto)
    {
        var price = new Price
        {
            EquipmentId = dto.EquipmentId,
            ModelId = dto.ModelId,
            ItemId = dto.ItemId,
            BasePriceInr = dto.BasePriceInr,
            EffectiveFrom = dto.EffectiveFrom ?? DateTime.UtcNow
        };

        _context.Prices.Add(price);
        await _context.SaveChangesAsync();

        var equipment = dto.EquipmentId.HasValue ? await _context.Equipment.FindAsync(dto.EquipmentId) : null;
        var model = dto.ModelId.HasValue ? await _context.Models.FindAsync(dto.ModelId) : null;
        var item = dto.ItemId.HasValue ? await _context.ItemMasters.FindAsync(dto.ItemId) : null;

        var resultDto = new PriceDto
        {
            PriceId = price.PriceId,
            EquipmentId = price.EquipmentId,
            ModelId = price.ModelId,
            ItemId = price.ItemId,
            BasePriceInr = price.BasePriceInr,
            EffectiveFrom = price.EffectiveFrom,
            CreatedAt = price.CreatedAt,
            CreatedBy = price.CreatedBy,
            EquipmentName = equipment?.EquipmentName,
            ModelName = model?.ModelName,
            ItemName = item?.ItemName
        };

        return CreatedAtAction(nameof(GetPrice), new { id = price.PriceId }, resultDto);
    }

    // PUT: api/Price/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePrice(int id, UpdatePriceDto dto)
    {
        var price = await _context.Prices.FindAsync(id);

        if (price == null)
        {
            return NotFound();
        }

        price.BasePriceInr = dto.BasePriceInr;
        price.EffectiveFrom = dto.EffectiveFrom;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Price/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePrice(int id)
    {
        var price = await _context.Prices.FindAsync(id);

        if (price == null)
        {
            return NotFound();
        }

        _context.Prices.Remove(price);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
