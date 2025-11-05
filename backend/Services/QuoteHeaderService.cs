using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Services;

public class QuoteHeaderService : IQuoteHeaderService
{
    private readonly ParasonDbContext _context;

    public QuoteHeaderService(ParasonDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<QuoteHeaderDto>> GetAllAsync()
    {
        return await _context.QuoteHeaders
            .Select(q => MapToDto(q))
            .ToListAsync();
    }

    public async Task<QuoteHeaderDto?> GetByIdAsync(int quoteId, byte revision)
    {
        var quote = await _context.QuoteHeaders
            .Where(q => q.QuoteId == quoteId && q.QuoteRevision == revision)
            .FirstOrDefaultAsync();

        return quote == null ? null : MapToDto(quote);
    }

    public async Task<QuoteHeaderDto> CreateAsync(CreateQuoteHeaderDto dto)
    {
        var quote = new QuoteHeader
        {
            QuoteRevision = dto.QuoteRevision,
            QuoteNumber = dto.QuoteNumber,
            QuoteName = dto.QuoteName,
            CustomerName = dto.CustomerName,
            Status = dto.Status,
            Currency = dto.Currency,
            ValidityDays = dto.ValidityDays,
            Notes = dto.Notes
        };

        _context.QuoteHeaders.Add(quote);
        await _context.SaveChangesAsync();

        return MapToDto(quote);
    }

    public async Task<bool> UpdateAsync(int quoteId, byte revision, UpdateQuoteHeaderDto dto)
    {
        var quote = await _context.QuoteHeaders.FindAsync(quoteId, revision);

        if (quote == null)
            return false;

        quote.QuoteRevision = dto.QuoteRevision;
        quote.QuoteNumber = dto.QuoteNumber;
        quote.QuoteName = dto.QuoteName;
        quote.CustomerName = dto.CustomerName;
        quote.Status = dto.Status;
        quote.Currency = dto.Currency;
        quote.ValidityDays = dto.ValidityDays;
        quote.Notes = dto.Notes;
        quote.ModifiedAt = DateTime.UtcNow;
        quote.ModifiedBy = "System"; // TODO: Get from authenticated user

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int quoteId, byte revision)
    {
        var quote = await _context.QuoteHeaders.FindAsync(quoteId, revision);

        if (quote == null)
            return false;

        _context.QuoteHeaders.Remove(quote);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<PagedResult<QuoteHeaderDto>> GetPagedAsync(QuoteSearchDto searchDto)
    {
        var query = _context.QuoteHeaders.AsQueryable();

        // Apply search filters
        if (!string.IsNullOrWhiteSpace(searchDto.SearchTerm))
        {
            var searchTerm = searchDto.SearchTerm.ToLower();
            query = query.Where(q =>
                q.QuoteNumber.ToLower().Contains(searchTerm) ||
                q.QuoteName.ToLower().Contains(searchTerm) ||
                q.CustomerName.ToLower().Contains(searchTerm));
        }

        if (!string.IsNullOrWhiteSpace(searchDto.QuoteNumber))
        {
            query = query.Where(q => q.QuoteNumber.ToLower().Contains(searchDto.QuoteNumber.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(searchDto.QuoteName))
        {
            query = query.Where(q => q.QuoteName.ToLower().Contains(searchDto.QuoteName.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(searchDto.CustomerName))
        {
            query = query.Where(q => q.CustomerName.ToLower().Contains(searchDto.CustomerName.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(searchDto.Status))
        {
            query = query.Where(q => q.Status == searchDto.Status);
        }

        if (searchDto.CreatedFrom.HasValue)
        {
            query = query.Where(q => q.CreatedAt >= searchDto.CreatedFrom.Value);
        }

        if (searchDto.CreatedTo.HasValue)
        {
            query = query.Where(q => q.CreatedAt <= searchDto.CreatedTo.Value);
        }

        // Get total count before pagination
        var totalCount = await query.CountAsync();

        // Apply sorting
        query = searchDto.SortBy.ToLower() switch
        {
            "quotenumber" => searchDto.SortOrder.ToLower() == "asc"
                ? query.OrderBy(q => q.QuoteNumber)
                : query.OrderByDescending(q => q.QuoteNumber),
            "quotename" => searchDto.SortOrder.ToLower() == "asc"
                ? query.OrderBy(q => q.QuoteName)
                : query.OrderByDescending(q => q.QuoteName),
            "customername" => searchDto.SortOrder.ToLower() == "asc"
                ? query.OrderBy(q => q.CustomerName)
                : query.OrderByDescending(q => q.CustomerName),
            "status" => searchDto.SortOrder.ToLower() == "asc"
                ? query.OrderBy(q => q.Status)
                : query.OrderByDescending(q => q.Status),
            "modifiedat" => searchDto.SortOrder.ToLower() == "asc"
                ? query.OrderBy(q => q.ModifiedAt)
                : query.OrderByDescending(q => q.ModifiedAt),
            _ => searchDto.SortOrder.ToLower() == "asc"
                ? query.OrderBy(q => q.CreatedAt)
                : query.OrderByDescending(q => q.CreatedAt)
        };

        // Apply pagination
        var items = await query
            .Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
            .Take(searchDto.PageSize)
            .Select(q => MapToDto(q))
            .ToListAsync();

        return new PagedResult<QuoteHeaderDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = searchDto.PageNumber,
            PageSize = searchDto.PageSize
        };
    }

    private static QuoteHeaderDto MapToDto(QuoteHeader quote)
    {
        return new QuoteHeaderDto
        {
            QuoteId = quote.QuoteId,
            QuoteRevision = quote.QuoteRevision,
            QuoteNumber = quote.QuoteNumber,
            QuoteName = quote.QuoteName,
            CustomerName = quote.CustomerName,
            Status = quote.Status,
            Currency = quote.Currency,
            ValidityDays = quote.ValidityDays,
            Notes = quote.Notes,
            CreatedAt = quote.CreatedAt,
            CreatedBy = quote.CreatedBy,
            ModifiedAt = quote.ModifiedAt,
            ModifiedBy = quote.ModifiedBy
        };
    }
}
