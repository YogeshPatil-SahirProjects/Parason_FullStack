using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;

namespace Parason_Api.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly CPQDbContext _context;

        public EquipmentService(CPQDbContext context)
        {
                this._context = context;
        }

        public async Task<EquipmentDto> CreateAsync(CreateEquipmentDto dto, string createdBy)
        {
            var entity = new Equipment
            {
                EquipmentCode = dto.EquipmentCode,
                EquipmentName = dto.EquipmentName,
                Description = dto.Description,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            _context.Equipments.Add(entity);
            await _context.SaveChangesAsync();

            return new EquipmentDto
            {
                EquipmentID = entity.EquipmentID,
                EquipmentCode = entity.EquipmentCode,
                EquipmentName = entity.EquipmentName,
                Description = entity.Description,
                IsActive = entity.IsActive 
            };
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<EquipmentDto>> GetAllAsync(PaginationParams paginationParams)
        {
            throw new NotImplementedException();
        }

        public Task<EquipmentDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<EquipmentDto>> GetEquipmentsByProcessAsync(int processId)
        {
            throw new NotImplementedException();
        }

        public Task<List<SeriesDto>> GetSeriesByEquipmentId(int equipmentId)
        {
            var series = _context.Series
                        .Where(s => s.IsActive && s.EquipmentID == equipmentId)
                        .Include(s => s.Models)
                        .Select(s => new SeriesDto
                        {
                            SeriesID = s.SeriesID,
                            SeriesCode = s.SeriesCode,
                            SeriesName = s.SeriesName,
                            Description = s.Description,
                            IsActive = s.IsActive,
                            Models = s.Models
                                     .Select(m => new ModelDto
                                     {
                                         ModelID = m.ModelID,
                                         SeriesID = m.SeriesID,
                                         ModelCode = m.ModelCode,
                                         ModelName = m.ModelName,
                                         Description = m.Description,
                                         IsActive = m.IsActive,
                                     }).ToList()
                        }).ToListAsync();
            return series;
        }

        public Task<EquipmentDto?> UpdateAsync(int id, UpdateEquipmentDto dto, string modifiedBy)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EquipmentAttributeDto>> GetEquipmentAttributes(int equipmentId)
        {
            var result = await _context.EquipmentAttributes
                .Where(ea => ea.EquipmentID == equipmentId)
                .Include(ea => ea.AttributeDef)
                    .ThenInclude(ad => ad.ListValues)
                .Select(ea => new
                {
                    Attribute = ea,
                    Value = _context.EquipmentAttributeValues
                        .Include(v => v.ListValue)
                        .FirstOrDefault(v =>
                            v.EquipmentID == equipmentId &&
                            v.AttributeID == ea.AttributeID)
                })
                .Select(x => new EquipmentAttributeDto
                {
                    AttributeID = x.Attribute.AttributeID,
                    AttributeName = x.Attribute.AttributeDef.AttributeName,
                    DataType = x.Attribute.AttributeDef.DataType,
                    Unit = x.Attribute.Unit ?? x.Attribute.AttributeDef.UnitDefault,
                    IsEditable = x.Attribute.IsEditable,
                    IsRequired = x.Attribute.IsRequired,
                    AttributeCategory = x.Attribute.AttributeCategory,

                    // Values (handle nulls safely)
                    NumValue = x.Value != null ? x.Value.NumValue : null,
                    TextValue = x.Value != null ? x.Value.TextValue : null,
                    BoolValue = x.Value != null ? x.Value.BoolValue : null,
                    ListValue = x.Value != null && x.Value.ListValue != null
                                    ? x.Value.ListValue.AttributeValue
                                    : null,

                    // List options (if applicable)
                    ListOptions = x.Attribute.AttributeDef.DataType == "list"
                        ? x.Attribute.AttributeDef.ListValues
                            .Select(lv => new AttributeListValueDto
                            {
                                ListValueID = lv.ListValueID,
                                AttributeID = lv.AttributeID,
                                AttributeValue = lv.AttributeValue,
                                Display = lv.Display
                            })
                            .OrderBy(lv => lv.Display)
                            .ToList()
                        : null
                })
                .OrderBy(a => a.AttributeID)
                .ToListAsync();

            return result;
        }

    }
}
