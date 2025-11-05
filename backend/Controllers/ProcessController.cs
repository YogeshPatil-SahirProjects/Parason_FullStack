using Microsoft.AspNetCore.Mvc;
using Parason_Api.DTOs;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProcessController : ControllerBase
{
    private readonly IProcessService _processService;

    public ProcessController(IProcessService processService)
    {
        _processService = processService;
    }

    // GET: api/Process
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProcessDto>>> GetProcesses()
    {
        var processes = await _processService.GetAllAsync();
        return Ok(processes);
    }

    // GET: api/Process/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProcessDto>> GetProcess(int id)
    {
        var process = await _processService.GetByIdAsync(id);

        if (process == null)
        {
            return NotFound();
        }

        return Ok(process);
    }

    // POST: api/Process
    [HttpPost]
    public async Task<ActionResult<ProcessDto>> CreateProcess(CreateProcessDto dto)
    {
        var process = await _processService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetProcess), new { id = process.ProcessId }, process);
    }

    // PUT: api/Process/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProcess(int id, UpdateProcessDto dto)
    {
        var success = await _processService.UpdateAsync(id, dto);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/Process/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProcess(int id)
    {
        var success = await _processService.DeleteAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
