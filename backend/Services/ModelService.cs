using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;

namespace Parason_Api.Services
{
    public class ModelService : IModelService
    {
        private readonly CPQDbContext _context;

        public ModelService(CPQDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<ModelDto>> GetAllAsync(PaginationParams paginationParams)
        {
            var query = _context.Models
                .Include(m => m.Series)
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(paginationParams.SearchTerm))
            {
                query = query.Where(m =>
                    m.ModelCode.Contains(paginationParams.SearchTerm) ||
                    m.ModelName.Contains(paginationParams.SearchTerm) ||
                    m.Series.SeriesName.Contains(paginationParams.SearchTerm));
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(paginationParams.SortBy))
            {
                query = paginationParams.SortBy.ToLower() switch
                {
                    "modelcode" => paginationParams.SortDescending
                        ? query.OrderByDescending(m => m.ModelCode)
                        : query.OrderBy(m => m.ModelCode),
                    "modelname" => paginationParams.SortDescending
                        ? query.OrderByDescending(m => m.ModelName)
                        : query.OrderBy(m => m.ModelName),
                    _ => query
                };
            }

            var totalRecords = await query.CountAsync();

            var items = await query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .Select(m => new ModelDto
                {
                    ModelID = m.ModelID,
                    SeriesID = m.SeriesID,
                    ModelCode = m.ModelCode,
                    ModelName = m.ModelName,
                    Description = m.Description,
                    IsActive = m.IsActive,
                    SeriesName = m.Series.SeriesName,
                    BasePrice = _context.Prices
                        .Where(p => p.ModelID == m.ModelID)
                        .OrderByDescending(p => p.EffectiveFrom)
                        .Select(p => p.BasePriceINR)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return new PagedResponse<ModelDto>
            {
                Items = items,
                TotalCount = totalRecords,
                PageNumber = paginationParams.PageNumber,
                PageSize = paginationParams.PageSize
            };
        }

        public async Task<ModelDto?> GetByIdAsync(int id)
        {
            var model = await _context.Models
                .Include(m => m.Series)
                .FirstOrDefaultAsync(m => m.ModelID == id);

            if (model == null) return null;

            var basePrice = await _context.Prices
                .Where(p => p.ModelID == model.ModelID)
                .OrderByDescending(p => p.EffectiveFrom)
                .Select(p => p.BasePriceINR)
                .FirstOrDefaultAsync();

            return new ModelDto
            {
                ModelID = model.ModelID,
                SeriesID = model.SeriesID,
                ModelCode = model.ModelCode,
                ModelName = model.ModelName,
                Description = model.Description,
                IsActive = model.IsActive,
                SeriesName = model.Series.SeriesName,
                BasePrice = basePrice
            };
        }

        public async Task<ModelDto> CreateAsync(CreateModelDto dto, string createdBy)
        {
            var model = new Model
            {
                SeriesID = dto.SeriesID,
                ModelCode = dto.ModelCode,
                ModelName = dto.ModelName,
                Description = dto.Description,
                IsActive = dto.IsActive,
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow
            };

            _context.Models.Add(model);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(model.ModelID) ?? throw new Exception("Failed to create model");
        }

        public async Task<ModelDto?> UpdateAsync(int id, UpdateModelDto dto, string modifiedBy)
        {
            var model = await _context.Models.FindAsync(id);
            if (model == null) return null;

            model.SeriesID = dto.SeriesID;
            model.ModelCode = dto.ModelCode;
            model.ModelName = dto.ModelName;
            model.Description = dto.Description;
            model.IsActive = dto.IsActive;
            model.ModifiedBy = modifiedBy;
            model.ModifiedAt = DateTime.UtcNow;

            _context.Models.Update(model);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(model.ModelID);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model == null) return false;

            _context.Models.Remove(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ModelDto>> GetModelsBySeriesAsync(int seriesId)
        {
            var models = await _context.Models
                .Where(m => m.SeriesID == seriesId)
                .Include(m => m.Series)
                .Select(m => new ModelDto
                {
                    ModelID = m.ModelID,
                    SeriesID = m.SeriesID,
                    ModelCode = m.ModelCode,
                    ModelName = m.ModelName,
                    Description = m.Description,
                    IsActive = m.IsActive,
                    SeriesName = m.Series.SeriesName,
                    BasePrice = _context.Prices
                        .Where(p => p.ModelID == m.ModelID)
                        .OrderByDescending(p => p.EffectiveFrom)
                        .Select(p => p.BasePriceINR)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return models;
        }
    }
}
