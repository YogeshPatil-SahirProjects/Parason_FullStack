using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parason_Api.Models;
using Parason_Api.DTOs;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemMasterController : ControllerBase
{
    private readonly ParasonDbContext _context;

    public ItemMasterController(ParasonDbContext context)
    {
        _context = context;
    }

    // GET: api/ItemMaster
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemMasterDto>>> GetItemMasters()
    {
        var items = await _context.ItemMasters
            .Select(i => new ItemMasterDto
            {
                ItemId = i.ItemId,
                ItemCode = i.ItemCode,
                ItemName = i.ItemName,
                Uom = i.Uom,
                IsActive = i.IsActive,
                CreatedAt = i.CreatedAt,
                CreatedBy = i.CreatedBy,
                ModifiedAt = i.ModifiedAt,
                ModifiedBy = i.ModifiedBy
            })
            .ToListAsync();

        return Ok(items);
    }

    // GET: api/ItemMaster/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemMasterDto>> GetItemMaster(int id)
    {
        var item = await _context.ItemMasters
            .Where(i => i.ItemId == id)
            .Select(i => new ItemMasterDto
            {
                ItemId = i.ItemId,
                ItemCode = i.ItemCode,
                ItemName = i.ItemName,
                Uom = i.Uom,
                IsActive = i.IsActive,
                CreatedAt = i.CreatedAt,
                CreatedBy = i.CreatedBy,
                ModifiedAt = i.ModifiedAt,
                ModifiedBy = i.ModifiedBy
            })
            .FirstOrDefaultAsync();

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    // POST: api/ItemMaster
    [HttpPost]
    public async Task<ActionResult<ItemMasterDto>> CreateItemMaster(CreateItemMasterDto dto)
    {
        var item = new ItemMaster
        {
            ItemCode = dto.ItemCode,
            ItemName = dto.ItemName,
            Uom = dto.Uom,
            IsActive = dto.IsActive
        };

        _context.ItemMasters.Add(item);
        await _context.SaveChangesAsync();

        var resultDto = new ItemMasterDto
        {
            ItemId = item.ItemId,
            ItemCode = item.ItemCode,
            ItemName = item.ItemName,
            Uom = item.Uom,
            IsActive = item.IsActive,
            CreatedAt = item.CreatedAt,
            CreatedBy = item.CreatedBy,
            ModifiedAt = item.ModifiedAt,
            ModifiedBy = item.ModifiedBy
        };

        return CreatedAtAction(nameof(GetItemMaster), new { id = item.ItemId }, resultDto);
    }

    // PUT: api/ItemMaster/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItemMaster(int id, UpdateItemMasterDto dto)
    {
        var item = await _context.ItemMasters.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        item.ItemCode = dto.ItemCode;
        item.ItemName = dto.ItemName;
        item.Uom = dto.Uom;
        item.IsActive = dto.IsActive;
        item.ModifiedAt = DateTime.UtcNow;
        item.ModifiedBy = "System"; // TODO: Get from authenticated user

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/ItemMaster/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItemMaster(int id)
    {
        var item = await _context.ItemMasters.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        _context.ItemMasters.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
