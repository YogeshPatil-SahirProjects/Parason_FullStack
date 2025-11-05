using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PriceController : ControllerBase
{
    private readonly IPriceService _priceService;

    public PriceController(IPriceService priceService)
    {
        _priceService = priceService;
    }

    // GET: api/Price
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PriceDto>>> GetPrices()
    {
        var prices = await _priceService.GetAllAsync();
        return Ok(prices);
    }

    // GET: api/Price/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PriceDto>> GetPrice(int id)
    {
        var price = await _priceService.GetByIdAsync(id);

        if (price == null)
        {
            return NotFound();
        }

        return Ok(price);
    }

    // POST: api/Price
    [HttpPost]
    public async Task<ActionResult<PriceDto>> CreatePrice(CreatePriceDto dto)
    {
        var price = await _priceService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetPrice), new { id = price.PriceId }, price);
    }

    // PUT: api/Price/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePrice(int id, UpdatePriceDto dto)
    {
        var success = await _priceService.UpdateAsync(id, dto);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/Price/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePrice(int id)
    {
        var success = await _priceService.DeleteAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
