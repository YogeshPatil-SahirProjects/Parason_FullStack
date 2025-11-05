using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModelController : ControllerBase
{
    private readonly IModelService _modelService;

    public ModelController(IModelService modelService)
    {
        _modelService = modelService;
    }

    // GET: api/Model
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ModelDto>>> GetModels()
    {
        var models = await _modelService.GetAllAsync();
        return Ok(models);
    }

    // GET: api/Model/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ModelDto>> GetModel(int id)
    {
        var model = await _modelService.GetByIdAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        return Ok(model);
    }

    // POST: api/Model
    [HttpPost]
    public async Task<ActionResult<ModelDto>> CreateModel(CreateModelDto dto)
    {
        var model = await _modelService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetModel), new { id = model.ModelId }, model);
    }

    // PUT: api/Model/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateModel(int id, UpdateModelDto dto)
    {
        var success = await _modelService.UpdateAsync(id, dto);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/Model/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteModel(int id)
    {
        var success = await _modelService.DeleteAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
