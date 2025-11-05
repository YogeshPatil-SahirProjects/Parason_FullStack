using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemMasterController : ControllerBase
{
    private readonly IItemMasterService _itemMasterService;

    public ItemMasterController(IItemMasterService itemMasterService)
    {
        _itemMasterService = itemMasterService;
    }

    // GET: api/ItemMaster
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemMasterDto>>> GetItemMasters()
    {
        var items = await _itemMasterService.GetAllAsync();
        return Ok(items);
    }

    // GET: api/ItemMaster/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemMasterDto>> GetItemMaster(int id)
    {
        var item = await _itemMasterService.GetByIdAsync(id);

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
        var item = await _itemMasterService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetItemMaster), new { id = item.ItemId }, item);
    }

    // PUT: api/ItemMaster/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItemMaster(int id, UpdateItemMasterDto dto)
    {
        var success = await _itemMasterService.UpdateAsync(id, dto);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/ItemMaster/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItemMaster(int id)
    {
        var success = await _itemMasterService.DeleteAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
