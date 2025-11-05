using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;

namespace Parason_Api.Services
{
    public class AttributeDefService : IAttributeDefService
    {
        private readonly CPQDbContext _context;

        public AttributeDefService(CPQDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<AttributeDefDto>> GetAllAsync(PaginationParams paginationParams)
        {
            var query = _context.AttributeDefs.AsQueryable();

            // Apply search
            if (!string.IsNullOrEmpty(paginationParams.SearchTerm))
            {
                query = query.Where(a =>
                    a.AttributeCode.Contains(paginationParams.SearchTerm) ||
                    a.AttributeName.Contains(paginationParams.SearchTerm) ||
                    a.DataType.Contains(paginationParams.SearchTerm));
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(paginationParams.SortBy))
            {
                query = paginationParams.SortBy.ToLower() switch
                {
                    "attributecode" => paginationParams.SortDescending
                        ? query.OrderByDescending(a => a.AttributeCode)
                        : query.OrderBy(a => a.AttributeCode),
                    "attributename" => paginationParams.SortDescending
                        ? query.OrderByDescending(a => a.AttributeName)
                        : query.OrderBy(a => a.AttributeName),
                    _ => query
                };
            }

            var totalRecords = await query.CountAsync();

            var items = await query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .Select(a => new AttributeDefDto
                {
                    AttributeID = a.AttributeID,
                    AttributeCode = a.AttributeCode,
                    AttributeName = a.AttributeName,
                    DataType = a.DataType,
                    UnitDefault = a.UnitDefault,
                    Description = a.Description,
                    IsActive = a.IsActive
                })
                .ToListAsync();

            return new PagedResponse<AttributeDefDto>
            {
                Items = items,
                TotalCount = totalRecords,
                PageNumber = paginationParams.PageNumber,
                PageSize = paginationParams.PageSize
            };
        }

        public async Task<AttributeDefDto?> GetByIdAsync(int id)
        {
            var attribute = await _context.AttributeDefs.FindAsync(id);
            if (attribute == null) return null;

            return new AttributeDefDto
            {
                AttributeID = attribute.AttributeID,
                AttributeCode = attribute.AttributeCode,
                AttributeName = attribute.AttributeName,
                DataType = attribute.DataType,
                UnitDefault = attribute.UnitDefault,
                Description = attribute.Description,
                IsActive = attribute.IsActive
            };
        }

        public async Task<AttributeDefDto> CreateAsync(CreateAttributeDefDto dto, string createdBy)
        {
            var attribute = new AttributeDef
            {
                AttributeCode = dto.AttributeCode,
                AttributeName = dto.AttributeName,
                DataType = dto.DataType,
                UnitDefault = dto.UnitDefault,
                Description = dto.Description,
                IsActive = dto.IsActive,
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow
            };

            _context.AttributeDefs.Add(attribute);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(attribute.AttributeID) ?? throw new Exception("Failed to create attribute");
        }

        public async Task<AttributeDefDto?> UpdateAsync(int id, UpdateAttributeDefDto dto, string modifiedBy)
        {
            var attribute = await _context.AttributeDefs.FindAsync(id);
            if (attribute == null) return null;

            attribute.AttributeCode = dto.AttributeCode;
            attribute.AttributeName = dto.AttributeName;
            attribute.DataType = dto.DataType;
            attribute.UnitDefault = dto.UnitDefault;
            attribute.Description = dto.Description;
            attribute.IsActive = dto.IsActive;
            attribute.ModifiedBy = modifiedBy;
            attribute.ModifiedAt = DateTime.UtcNow;

            _context.AttributeDefs.Update(attribute);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(attribute.AttributeID);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var attribute = await _context.AttributeDefs.FindAsync(id);
            if (attribute == null) return false;

            _context.AttributeDefs.Remove(attribute);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
