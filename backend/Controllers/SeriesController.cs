using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeriesController : ControllerBase
{
    private readonly ISeriesService _seriesService;

    public SeriesController(ISeriesService seriesService)
    {
        _seriesService = seriesService;
    }

    // GET: api/Series
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SeriesDto>>> GetSeries()
    {
        var series = await _seriesService.GetAllAsync();
        return Ok(series);
    }

    // GET: api/Series/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SeriesDto>> GetSeries(int id)
    {
        var series = await _seriesService.GetByIdAsync(id);

        if (series == null)
        {
            return NotFound();
        }

        return Ok(series);
    }

    // POST: api/Series
    [HttpPost]
    public async Task<ActionResult<SeriesDto>> CreateSeries(CreateSeriesDto dto)
    {
        var series = await _seriesService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetSeries), new { id = series.SeriesId }, series);
    }

    // PUT: api/Series/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSeries(int id, UpdateSeriesDto dto)
    {
        var success = await _seriesService.UpdateAsync(id, dto);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/Series/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSeries(int id)
    {
        var success = await _seriesService.DeleteAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
