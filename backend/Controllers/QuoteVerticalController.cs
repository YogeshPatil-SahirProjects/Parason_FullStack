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

    // GET: api/QuoteVertical/VerticalConfig/5/0/1
    [HttpGet("VerticalConfig/{quoteId}/{quoteRevision}/{verticalId}")]
    public async Task<ActionResult<VerticalConfigDto>> GetVerticalConfig(int quoteId, byte quoteRevision, int verticalId)
    {
        // Get all quote verticals for this quote and vertical
        var quoteVerticals = await _context.QuoteVerticals
            .Where(qv => qv.QuoteId == quoteId && qv.QuoteRevision == quoteRevision && qv.VerticalId == verticalId)
            .Include(qv => qv.Vertical)
            .Include(qv => qv.Process)
            .Include(qv => qv.QuoteEquipmentOrModels)
                .ThenInclude(qeom => qeom.Equipment)
            .Include(qv => qv.QuoteEquipmentOrModels)
                .ThenInclude(qeom => qeom.Series)
            .Include(qv => qv.QuoteEquipmentOrModels)
                .ThenInclude(qeom => qeom.Model)
            .Include(qv => qv.ScopeOfSupplies)
                .ThenInclude(sos => sos.Item)
            .Include(qv => qv.ScopeOfSupplies)
                .ThenInclude(sos => sos.Model)
            .Include(qv => qv.SpecDetails)
                .ThenInclude(sd => sd.Attribute)
            .Include(qv => qv.SpecDetails)
                .ThenInclude(sd => sd.ListValue)
            .ToListAsync();

        if (!quoteVerticals.Any())
        {
            return NotFound();
        }

        var vertical = quoteVerticals.First().Vertical;

        // Group by process
        var processList = new List<ProcessConfigDto>();
        var processGroups = quoteVerticals.GroupBy(qv => qv.ProcessId);

        foreach (var processGroup in processGroups)
        {
            var process = processGroup.First().Process;

            // Get all equipment for this process via linking table
            var processEquipment = await _context.LProcessEquipments
                .Where(lpe => lpe.ProcessId == process.ProcessId && lpe.IsActive)
                .Include(lpe => lpe.Equipment)
                    .ThenInclude(e => e.Series)
                        .ThenInclude(s => s.Models)
                .ToListAsync();

            var equipmentList = new List<EquipmentConfigDto>();

            foreach (var lpe in processEquipment)
            {
                var equipment = lpe.Equipment;
                var seriesList = new List<SeriesConfigDto>();

                foreach (var series in equipment.Series.Where(s => s.IsActive))
                {
                    var modelsList = new List<ModelConfigDto>();

                    // Get models that are configured in QuoteEquipmentOrModel
                    var configuredModels = processGroup
                        .SelectMany(qv => qv.QuoteEquipmentOrModels)
                        .Where(qeom => qeom.SeriesId == series.SeriesId && qeom.ModelId != null)
                        .ToList();

                    foreach (var qeom in configuredModels)
                    {
                        var model = qeom.Model;
                        if (model != null && model.IsActive)
                        {
                            modelsList.Add(new ModelConfigDto
                            {
                                ModelID = model.ModelId,
                                SeriesID = model.SeriesId,
                                ModelCode = model.ModelCode,
                                ModelName = model.ModelName,
                                Description = model.Description,
                                IsActive = model.IsActive,
                                SeriesName = series.SeriesName,
                                BasePrice = qeom.PriceInr,
                                Quantity = qeom.Quantity
                            });
                        }
                    }

                    if (modelsList.Any())
                    {
                        seriesList.Add(new SeriesConfigDto
                        {
                            SeriesID = series.SeriesId,
                            SeriesCode = series.SeriesCode,
                            SeriesName = series.SeriesName,
                            Description = series.Description,
                            IsActive = series.IsActive,
                            Models = modelsList
                        });
                    }
                }

                if (seriesList.Any())
                {
                    equipmentList.Add(new EquipmentConfigDto
                    {
                        EquipmentID = equipment.EquipmentId,
                        EquipmentCode = equipment.EquipmentCode,
                        EquipmentName = equipment.EquipmentName,
                        Description = equipment.Description,
                        IsActive = equipment.IsActive,
                        Series = seriesList
                    });
                }
            }

            // Get scope of supply items for this process
            var scopeItems = processGroup
                .SelectMany(qv => qv.ScopeOfSupplies)
                .Select(sos => new ScopeOfSupplyConfigDto
                {
                    RecordID = sos.RecordId,
                    ModelID = sos.ModelId ?? 0,
                    ItemId = sos.ItemId,
                    Price_INR = sos.PriceInr ?? 0,
                    Quantity = sos.Quantity,
                    ItemName = sos.Item.ItemName,
                    ItemCode = sos.Item.ItemCode,
                    Description = sos.Item.ItemName,
                    IsMandatory = true,
                    ModelName = sos.Model?.ModelName
                })
                .ToList();

            // Get specifications for this process
            var specifications = processGroup
                .SelectMany(qv => qv.SpecDetails)
                .Select(sd => new SpecDetailConfigDto
                {
                    RecordID = sd.RecordId,
                    EquipmentID = sd.EquipmentId ?? 0,
                    ModelID = sd.ModelId ?? 0,
                    AttributeID = sd.AttributeId,
                    ListValueID = sd.ListValueId ?? 0,
                    NumValue = sd.NumValue ?? 0,
                    TextValue = sd.TextValue ?? "",
                    BoolValue = sd.BoolValue ?? false,
                    AttributeName = sd.Attribute.AttributeName,
                    DataType = sd.Attribute.DataType,
                    ListValueDisplay = sd.ListValue?.Display ?? ""
                })
                .ToList();

            processList.Add(new ProcessConfigDto
            {
                ProcessID = process.ProcessId,
                ProcessCode = process.ProcessCode,
                ProcessName = process.ProcessName,
                Description = process.Description,
                IsActive = process.IsActive,
                Equipments = equipmentList,
                ScopeItems = scopeItems,
                Specifications = specifications
            });
        }

        // Calculate total price
        var totalPrice = quoteVerticals
            .SelectMany(qv => qv.QuoteEquipmentOrModels)
            .Sum(qeom => (qeom.PriceInr ?? 0) * qeom.Quantity) +
            quoteVerticals
            .SelectMany(qv => qv.ScopeOfSupplies)
            .Sum(sos => (sos.PriceInr ?? 0) * sos.Quantity);

        var result = new VerticalConfigDto
        {
            VerticalID = vertical.VerticalId,
            VerticalName = vertical.VerticalName,
            Layer = quoteVerticals.First().Layer,
            Total_Price = totalPrice,
            Processes = processList
        };

        return Ok(result);
    }
}
