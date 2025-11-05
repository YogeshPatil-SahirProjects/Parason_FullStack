using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services.Interfaces;

namespace Parason_Api.Services;

public class QuoteVerticalService : IQuoteVerticalService
{
    private readonly ParasonDbContext _context;

    public QuoteVerticalService(ParasonDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<QuoteVerticalDto>> GetAllAsync()
    {
        return await _context.QuoteVerticals
            .Include(q => q.Vertical)
            .Include(q => q.Process)
            .Select(q => MapToDto(q))
            .ToListAsync();
    }

    public async Task<QuoteVerticalDto?> GetByIdAsync(int id)
    {
        var item = await _context.QuoteVerticals
            .Include(q => q.Vertical)
            .Include(q => q.Process)
            .FirstOrDefaultAsync(q => q.RecordId == id);
        return item == null ? null : MapToDto(item);
    }

    public async Task<QuoteVerticalDto> CreateAsync(CreateQuoteVerticalDto dto)
    {
        var item = new QuoteVertical
        {
            QuoteId = dto.QuoteId,
            QuoteRevision = dto.QuoteRevision,
            Layer = dto.Layer,
            VerticalId = dto.VerticalId,
            ProcessId = dto.ProcessId
        };

        _context.QuoteVerticals.Add(item);
        await _context.SaveChangesAsync();

        var created = await _context.QuoteVerticals
            .Include(q => q.Vertical)
            .Include(q => q.Process)
            .FirstAsync(q => q.RecordId == item.RecordId);

        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, UpdateQuoteVerticalDto dto)
    {
        var item = await _context.QuoteVerticals.FindAsync(id);

        if (item == null)
            return false;

        item.QuoteId = dto.QuoteId;
        item.QuoteRevision = dto.QuoteRevision;
        item.Layer = dto.Layer;
        item.VerticalId = dto.VerticalId;
        item.ProcessId = dto.ProcessId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _context.QuoteVerticals.FindAsync(id);

        if (item == null)
            return false;

        _context.QuoteVerticals.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    private static QuoteVerticalDto MapToDto(QuoteVertical item)
    {
        return new QuoteVerticalDto
        {
            RecordId = item.RecordId,
            QuoteId = item.QuoteId,
            QuoteRevision = item.QuoteRevision,
            Layer = item.Layer,
            VerticalId = item.VerticalId,
            ProcessId = item.ProcessId,
            CreatedAt = item.CreatedAt,
            CreatedBy = item.CreatedBy,
            VerticalName = item.Vertical?.VerticalName,
            ProcessName = item.Process?.ProcessName
        };
    }
}
