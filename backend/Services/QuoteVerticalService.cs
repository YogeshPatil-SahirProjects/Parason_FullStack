using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services;
using static Parason_Api.DTOs.QuoteVerticalConfigurationDto;

public class QuoteVerticalService : IQuoteVerticalService
{
    private readonly CPQDbContext _context;

    public QuoteVerticalService(CPQDbContext context)
    {
        _context = context;
    }

    public async Task<List<QuoteVerticalDto>> GetByQuoteIdAsync(int quoteId, byte revision)
    {
        var records = await _context.QuoteVerticals
            .Where(qv => qv.QuoteID == quoteId && qv.QuoteRevision == revision)
            .Include(qv => qv.VerticalArea)
            .Include(qv => qv.Process)
            .ToListAsync();

        var result = new List<QuoteVerticalDto>();

        foreach (var rec in records)
        {
            var dto = new QuoteVerticalDto
            {
                RecordID = rec.RecordID,
                QuoteID = rec.QuoteID,
                QuoteRevision = rec.QuoteRevision,
                Layer = rec.Layer,
                VerticalID = rec.VerticalID,
                ProcessID = rec.ProcessID,
                VerticalName = rec.VerticalArea?.VerticalName,
                ProcessName = rec.Process?.ProcessName,
                EquipmentsOrModels = await _context.QuoteEquipmentOrModels
                    .Where(qe => qe.RecordID == rec.RecordID)
                    .Select(qe => new QuoteEquipmentOrModelDto
                    {
                        QEOMId = qe.QEOMId,
                        EquipmentID = qe.EquipmentID,
                        SeriesID = qe.SeriesID,
                        ModelID = qe.ModelID,
                        Price_INR = qe.Price_INR,
                        Quantity = qe.Quantity
                    }).ToListAsync(),
                ScopeItems = await _context.ScopeOfSupplies
                    .Where(s => s.RecordID == rec.RecordID)
                    .Select(s => new ScopeOfSupplyDto
                    {
                        RecordID = s.RecordID,
                        ModelID = s.ModelID,
                        ItemId = s.ItemId,
                        Price_INR = s.Price_INR,
                        Quantity = s.Quantity
                    }).ToListAsync(),
                Specifications = await _context.SpecDetails
                    .Where(sd => sd.RecordID == rec.RecordID)
                    .Select(sd => new SpecDetailsDto
                    {
                        RecordID = sd.RecordID,
                        EquipmentID = sd.EquipmentID,
                        ModelID = sd.ModelID,
                        AttributeID = sd.AttributeID,
                        ListValueID = sd.ListValueID,
                        NumValue = sd.NumValue,
                        TextValue = sd.TextValue,
                        BoolValue = sd.BoolValue
                    }).ToListAsync()
            };

            result.Add(dto);
        }

        return result;
    }

    public async Task<QuoteVerticalHierarchyDto?> GetVerticalsInfoAsync(int quoteId, byte quoteRevision, int verticalId)
    {
        // Step 1: Get all QuoteVertical rows for the same Vertical (multiple processes)
        var allProcesses = await _context.QuoteVerticals
            .Include(qv => qv.VerticalArea)
            .Include(qv => qv.Process)
            .Include(qv => qv.QuoteEquipmentOrModels)
            .Where(qv => qv.QuoteID == quoteId
                         && qv.QuoteRevision == quoteRevision
                         && qv.VerticalID == verticalId)
            .ToListAsync();

        if (!allProcesses.Any())
            return null;

        var recordIds = allProcesses.Select(p => p.RecordID).ToList();
        var verticalInfo = allProcesses.First();

        // Step 2: Preload all child entities
        var allEquipments = await _context.QuoteEquipmentOrModels
            .Include(qe => qe.Equipment)
            .Include(qe => qe.Series)
            .Include(qe => qe.Model)
            .Where(qe => recordIds.Contains(qe.RecordID))
            .ToListAsync();

        var allScopes = await _context.ScopeOfSupplies
            .Include(s => s.Item)
            .Include(s => s.Model)
            .Where(s => recordIds.Contains(s.RecordID))
            .ToListAsync();

        var allSpecs = await _context.SpecDetails
            .Include(sd => sd.AttributeDef)
            .Include(sd => sd.ListValue)
            .Where(sd => recordIds.Contains(sd.RecordID))
            .ToListAsync();

        // Step 3: Build hierarchy
        var verticalDto = new QuoteVerticalHierarchyDto
        {
            VerticalID = verticalId,
            VerticalName = verticalInfo.VerticalArea?.VerticalName,
            Layer = verticalInfo.Layer,
            Total_Price = allEquipments?.Sum(qem => qem.Price_INR ?? 0m) ?? 0m,
            Processes = allProcesses.Select(proc => new ProcessHierarchyDto
            {
                ProcessID = proc.ProcessID,
                ProcessCode = proc.Process.ProcessCode,
                ProcessName = proc.Process.ProcessName,
                Description = proc.Process.Description,
                IsActive = proc.Process.IsActive,

                // Equipments
                Equipments = allEquipments
                    .Where(eq => eq.RecordID == proc.RecordID && eq.EquipmentID != null)
                    .GroupBy(eq => eq.EquipmentID)
                    .Select(g => new EquipmentHierarchyDto
                    {
                        EquipmentID = g.First().Equipment!.EquipmentID,
                        EquipmentCode = g.First().Equipment!.EquipmentCode,
                        EquipmentName = g.First().Equipment!.EquipmentName,
                        Description = g.First().Equipment!.Description,
                        IsActive = g.First().Equipment!.IsActive,

                        // Get price for equipment-only entries (no series, no model)
                        EquipmentOnlyItems = g.Where(x => x.SeriesID == null && x.ModelID == null)
                        .Select(x => new PriceQuantityDto
                        {
                            QEOMId = x.QEOMId,
                            Price_INR = x.Price_INR,
                            Quantity = x.Quantity
                        }).ToList(),

                        Series = g.Where(x => x.Series != null)
                            .GroupBy(x => x.Series!.SeriesID)
                            .Select(sg => new SeriesHierarchyDto
                            {
                                SeriesID = sg.First().Series!.SeriesID,
                                SeriesCode = sg.First().Series!.SeriesCode,
                                SeriesName = sg.First().Series!.SeriesName,
                                Description = sg.First().Series!.Description,
                                IsActive = sg.First().Series!.IsActive,
                                Models = sg.Where(x => x.Model != null)
                                    .Select(m => new ModelDto
                                    {
                                        ModelID = m.Model!.ModelID,
                                        SeriesID = m.Model.SeriesID,
                                        ModelCode = m.Model.ModelCode,
                                        ModelName = m.Model.ModelName,
                                        Description = m.Model.Description,
                                        IsActive = m.Model.IsActive,
                                        SeriesName = m.Model.Series?.SeriesName
                                    })
                                    .ToList()
                            })
                            .ToList()
                    })
                    .ToList(),

                // Scope Items
                ScopeItems = allScopes
                    .Where(s => s.RecordID == proc.RecordID)
                    .Select(s => new ScopeOfSupplyDto
                    {
                        RecordID = s.RecordID,
                        ModelID = s.ModelID,
                        ItemId = s.ItemId,
                        ItemName = s.Item.ItemName,
                        ItemCode = s.Item.ItemCode,
                        Price_INR = s.Price_INR,
                        Quantity = s.Quantity,
                        Description = s.Item.Description,
                        IsMandatory = s.IsMandatory,
                        ModelName = s.Model?.ModelName
                    }).ToList(),

                // Specifications
                Specifications = allSpecs
                    .Where(sd => sd.RecordID == proc.RecordID)
                    .Select(sd => new SpecDetailsDto
                    {
                        RecordID = sd.RecordID,
                        EquipmentID = sd.EquipmentID,
                        ModelID = sd.ModelID,
                        AttributeID = sd.AttributeID,
                        AttributeName = sd.AttributeDef.AttributeName,
                        DataType = sd.AttributeDef.DataType,
                        ListValueID = sd.ListValueID,
                        ListValueDisplay = sd.ListValue != null ? sd.ListValue.Display : null,
                        NumValue = sd.NumValue,
                        TextValue = sd.TextValue,
                        BoolValue = sd.BoolValue
                    }).ToList()
            }).ToList()
        };

        return verticalDto;
    }



    public async Task<QuoteVerticalDto?> GetByRecordIdAsync(int recordId)
    {
        var rec = await _context.QuoteVerticals
            .Include(qv => qv.VerticalArea)
            .Include(qv => qv.Process)
            .FirstOrDefaultAsync(qv => qv.RecordID == recordId);

        if (rec == null) return null;

        return new QuoteVerticalDto
        {
            RecordID = rec.RecordID,
            QuoteID = rec.QuoteID,
            QuoteRevision = rec.QuoteRevision,
            Layer = rec.Layer,
            VerticalID = rec.VerticalID,
            ProcessID = rec.ProcessID,
            VerticalName = rec.VerticalArea?.VerticalName,
            ProcessName = rec.Process?.ProcessName,
            EquipmentsOrModels = await _context.QuoteEquipmentOrModels
                .Where(qe => qe.RecordID == rec.RecordID)
                .Select(qe => new QuoteEquipmentOrModelDto
                {
                    QEOMId = qe.QEOMId,
                    EquipmentID = qe.EquipmentID,
                    SeriesID = qe.SeriesID,
                    ModelID = qe.ModelID,
                    Price_INR = qe.Price_INR,
                    Quantity = qe.Quantity
                }).ToListAsync(),
            ScopeItems = await _context.ScopeOfSupplies
                .Where(s => s.RecordID == rec.RecordID)
                .Select(s => new ScopeOfSupplyDto
                {
                    RecordID = s.RecordID,
                    ModelID = s.ModelID,
                    ItemId = s.ItemId,
                    Price_INR = s.Price_INR,
                    Quantity = s.Quantity
                }).ToListAsync(),
            Specifications = await _context.SpecDetails
                .Where(sd => sd.RecordID == rec.RecordID)
                .Select(sd => new SpecDetailsDto
                {
                    RecordID = sd.RecordID,
                    EquipmentID = sd.EquipmentID,
                    ModelID = sd.ModelID,
                    AttributeID = sd.AttributeID,
                    ListValueID = sd.ListValueID,
                    NumValue = sd.NumValue,
                    TextValue = sd.TextValue,
                    BoolValue = sd.BoolValue
                }).ToListAsync()
        };
    }

    public async Task<QuoteVerticalDto> CreateAsync(CreateQuoteVerticalDto dto, string createdBy)
    {
        try
        {
            var entity = new QuoteVertical
            {
                QuoteID = dto.QuoteID,
                QuoteRevision = dto.QuoteRevision,
                Layer = dto.Layer,
                VerticalID = dto.VerticalID,
                ProcessID = dto.ProcessID,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            _context.QuoteVerticals.Add(entity);
            await _context.SaveChangesAsync();

            var vertical = await _context.VerticalAreas.FindAsync(dto.VerticalID);
            var process = await _context.Processes.FindAsync(dto.ProcessID);

            return new QuoteVerticalDto
            {
                RecordID = entity.RecordID,
                QuoteID = entity.QuoteID,
                QuoteRevision = entity.QuoteRevision,
                Layer = entity.Layer,
                VerticalID = entity.VerticalID,
                ProcessID = entity.ProcessID,
                VerticalName = vertical?.VerticalName,
                ProcessName = process?.ProcessName,
                EquipmentsOrModels = new List<QuoteEquipmentOrModelDto>(),
                ScopeItems = new List<ScopeOfSupplyDto>(),
                Specifications = new List<SpecDetailsDto>()
            };

        }
        catch (Exception ex)
        {
            throw new Exception("Error creating QuoteVertical", ex);
        }        
    }

    public async Task<bool> DeleteAsync(int recordId)
    {
        var entity = await _context.QuoteVerticals.FindAsync(recordId);
        if (entity == null) return false;

        _context.QuoteVerticals.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<QuoteVerticalDto> UpdateAsync(int recordId, QuoteVerticalDto dto)
    {        
        var entity = await _context.QuoteVerticals
            .FirstOrDefaultAsync(qv => qv.RecordID == recordId);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Quote vertical with RecordID {recordId} not found.");
        }

        entity.Layer = dto.Layer;
        entity.VerticalID = dto.VerticalID;
        entity.ProcessID = dto.ProcessID;

        await _context.SaveChangesAsync();

        return await GetByRecordIdAsync(recordId)
        ?? throw new InvalidOperationException("Failed to retrieve updated entity");          
    }
}
