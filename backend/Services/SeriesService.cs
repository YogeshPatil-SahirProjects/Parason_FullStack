using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;

namespace Parason_Api.Services
{
    public class SeriesService : ISeriesService
    {
        private readonly CPQDbContext _context;

        public SeriesService(CPQDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<SeriesDto>> GetAllAsync(PaginationParams paginationParams)
        {
            var query = _context.Series
                .Include(s => s.Equipment)
                .Include(s => s.Models)
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(paginationParams.SearchTerm))
            {
                query = query.Where(s =>
                    s.SeriesCode.Contains(paginationParams.SearchTerm) ||
                    s.SeriesName.Contains(paginationParams.SearchTerm) ||
                    s.Equipment.EquipmentName.Contains(paginationParams.SearchTerm));
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(paginationParams.SortBy))
            {
                query = paginationParams.SortBy.ToLower() switch
                {
                    "seriescode" => paginationParams.SortDescending
                        ? query.OrderByDescending(s => s.SeriesCode)
                        : query.OrderBy(s => s.SeriesCode),
                    "seriesname" => paginationParams.SortDescending
                        ? query.OrderByDescending(s => s.SeriesName)
                        : query.OrderBy(s => s.SeriesName),
                    _ => query
                };
            }

            var totalRecords = await query.CountAsync();

            var items = await query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .Select(s => new SeriesDto
                {
                    SeriesID = s.SeriesID,
                    EquipmentID = s.EquipmentID,
                    SeriesCode = s.SeriesCode,
                    SeriesName = s.SeriesName,
                    Description = s.Description,
                    IsActive = s.IsActive,
                    EquipmentName = s.Equipment.EquipmentName,
                    Models = s.Models.Select(m => new ModelDto
                    {
                        ModelID = m.ModelID,
                        ModelCode = m.ModelCode,
                        ModelName = m.ModelName,
                        Description = m.Description,
                        IsActive = m.IsActive
                    }).ToList()
                })
                .ToListAsync();

            return new PagedResponse<SeriesDto>
            {
                Items = items,
                TotalCount = totalRecords,
                PageNumber = paginationParams.PageNumber,
                PageSize = paginationParams.PageSize
            };
        }

        public async Task<SeriesDto?> GetByIdAsync(int id)
        {
            var series = await _context.Series
                .Include(s => s.Equipment)
                .Include(s => s.Models)
                .FirstOrDefaultAsync(s => s.SeriesID == id);

            if (series == null) return null;

            return new SeriesDto
            {
                SeriesID = series.SeriesID,
                EquipmentID = series.EquipmentID,
                SeriesCode = series.SeriesCode,
                SeriesName = series.SeriesName,
                Description = series.Description,
                IsActive = series.IsActive,
                EquipmentName = series.Equipment.EquipmentName,
                Models = series.Models.Select(m => new ModelDto
                {
                    ModelID = m.ModelID,
                    ModelCode = m.ModelCode,
                    ModelName = m.ModelName,
                    Description = m.Description,
                    IsActive = m.IsActive
                }).ToList()
            };
        }

        public async Task<SeriesDto> CreateAsync(CreateSeriesDto dto, string createdBy)
        {
            var series = new Series
            {
                EquipmentID = dto.EquipmentID,
                SeriesCode = dto.SeriesCode,
                SeriesName = dto.SeriesName,
                Description = dto.Description,
                IsActive = dto.IsActive,
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow
            };

            _context.Series.Add(series);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(series.SeriesID) ?? throw new Exception("Failed to create series");
        }

        public async Task<SeriesDto?> UpdateAsync(int id, UpdateSeriesDto dto, string modifiedBy)
        {
            var series = await _context.Series.FindAsync(id);
            if (series == null) return null;

            series.EquipmentID = dto.EquipmentID;
            series.SeriesCode = dto.SeriesCode;
            series.SeriesName = dto.SeriesName;
            series.Description = dto.Description;
            series.IsActive = dto.IsActive;
            series.ModifiedBy = modifiedBy;
            series.ModifiedAt = DateTime.UtcNow;

            _context.Series.Update(series);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(series.SeriesID);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var series = await _context.Series.FindAsync(id);
            if (series == null) return false;

            _context.Series.Remove(series);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<SeriesDto>> GetSeriesByEquipmentAsync(int equipmentId)
        {
            var seriesList = await _context.Series
                .Where(s => s.EquipmentID == equipmentId)
                .Include(s => s.Models)
                .Include(s => s.Equipment)
                .Select(s => new SeriesDto
                {
                    SeriesID = s.SeriesID,
                    EquipmentID = s.EquipmentID,
                    SeriesCode = s.SeriesCode,
                    SeriesName = s.SeriesName,
                    Description = s.Description,
                    IsActive = s.IsActive,
                    EquipmentName = s.Equipment.EquipmentName,
                    Models = s.Models.Select(m => new ModelDto
                    {
                        ModelID = m.ModelID,
                        ModelCode = m.ModelCode,
                        ModelName = m.ModelName,
                        Description = m.Description,
                        IsActive = m.IsActive
                    }).ToList()
                })
                .ToListAsync();

            return seriesList;
        }

        public Task<List<ModelDto>> GetModelsBySeriesId(int seriesId)
        {
            var Models = _context.Models
                        .Where(m => m.IsActive && m.SeriesID == seriesId)
                        .Include(m => m.Prices)
                        .Select(m => new ModelDto {
                                ModelID = m.ModelID,
                                SeriesID = m.SeriesID,
                                ModelName = m.ModelName,
                                ModelCode = m.ModelCode,
                                Description = m.Description,
                                IsActive = m.IsActive,
                                BasePrice = m.Prices
                                            .Select(p => p.BasePriceINR)
                                            .FirstOrDefault()
                                            
                         }).ToListAsync();

            return Models;
        }
    }
}
