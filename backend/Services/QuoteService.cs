using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;

namespace Parason_Api.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly CPQDbContext _context;

        public QuoteService(CPQDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<QuoteHeaderDto>> GetAllAsync(PaginationParams paginationParams)
        {
            var query = _context.QuoteHeaders
                .Include(q => q.QuoteVerticals) // 👈 Include related verticals
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(paginationParams.SearchTerm))
            {
                var searchTerm = paginationParams.SearchTerm.ToLower();
                query = query.Where(q =>
                    q.QuoteNumber.ToLower().Contains(searchTerm) ||
                    q.QuoteName.ToLower().Contains(searchTerm) ||
                    q.Status.ToLower().Contains(searchTerm) ||
                    q.CreatedBy.ToLower().Contains(searchTerm) ||
                    q.CustomerName.ToLower().Contains(searchTerm));
            }

            // Get total count
            var totalCount = await query.CountAsync();

            // Apply sorting
            query = paginationParams.SortBy?.ToLower() switch
            {
                "quotenumber" => paginationParams.SortDescending
                    ? query.OrderByDescending(q => q.QuoteNumber)
                    : query.OrderBy(q => q.QuoteNumber),
                "customer" => paginationParams.SortDescending
                    ? query.OrderByDescending(q => q.CustomerName)
                    : query.OrderBy(q => q.CustomerName),
                "date" => paginationParams.SortDescending
                    ? query.OrderByDescending(q => q.CreatedAt)
                    : query.OrderBy(q => q.CreatedAt),
                _ => query.OrderByDescending(q => q.CreatedAt)
            };

            // Apply pagination and project to DTO
            var items = await query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .Select(q => new QuoteHeaderDto
                {
                    QuoteID = q.QuoteID,
                    QuoteRevision = q.QuoteRevision,
                    QuoteNumber = q.QuoteNumber,
                    QuoteName = q.QuoteName,
                    CustomerName = q.CustomerName,
                    Status = q.Status,
                    Currency = q.Currency,
                    ValidityDays = q.ValidityDays,
                    Notes = q.Notes,
                    CreatedAt = q.CreatedAt,
                    CreatedBy = q.CreatedBy,
                    QuoteVerticals = q.QuoteVerticals.Select(v => new QuoteVerticalDto
                    { 
                        VerticalID = v.VerticalID,
                        VerticalName = v.VerticalArea.VerticalName
                    }).ToList()
                })
                .ToListAsync();

            return new PagedResponse<QuoteHeaderDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = paginationParams.PageNumber,
                PageSize = paginationParams.PageSize
            };
        }


        public async Task<QuoteHeaderDto?> GetByIdAsync(int quoteId, byte revision)
        {
            var quote = await _context.QuoteHeaders
                .Where(q => q.QuoteID == quoteId && q.QuoteRevision == revision)
                .Include(q => q.QuoteVerticals)
                .Select(q => new QuoteHeaderDto
                {
                    QuoteID = q.QuoteID,
                    QuoteRevision = q.QuoteRevision,
                    QuoteNumber = q.QuoteNumber,
                    QuoteName = q.QuoteName,
                    CustomerName = q.CustomerName,
                    Status = q.Status,
                    Currency = q.Currency,
                    ValidityDays = q.ValidityDays,
                    Notes = q.Notes,
                    CreatedAt = q.CreatedAt,
                    CreatedBy = q.CreatedBy,
                    QuoteVerticals = q.QuoteVerticals.Select(qv => new QuoteVerticalDto
                    {
                        RecordID = qv.RecordID,
                        QuoteID = qv.QuoteID,
                        QuoteRevision = qv.QuoteRevision,
                        Layer = qv.Layer,
                        VerticalID = qv.VerticalID,
                        ProcessID = qv.ProcessID,
                        VerticalName = qv.VerticalArea.VerticalName,
                        ProcessName = qv.Process.ProcessName
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return quote;
        }

        public async Task<QuoteHeaderDto> CreateAsync(CreateQuoteHeaderDto dto, string createdBy)
        {
            var quote = new QuoteHeader
            {
                QuoteNumber = dto.QuoteNumber,
                QuoteName = dto.QuoteName,
                CustomerName = dto.CustomerName,
                Status = dto.Status,
                Currency = dto.Currency,
                ValidityDays = dto.ValidityDays,
                Notes = dto.Notes,
                QuoteRevision = 0,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            _context.QuoteHeaders.Add(quote);
            await _context.SaveChangesAsync();

            return new QuoteHeaderDto
            {
                QuoteID = quote.QuoteID,
                QuoteRevision = quote.QuoteRevision,
                QuoteNumber = quote.QuoteNumber,
                QuoteName = quote.QuoteName,
                CustomerName = quote.CustomerName,
                Status = quote.Status,
                Currency = quote.Currency,
                ValidityDays = quote.ValidityDays,
                Notes = quote.Notes,
                CreatedAt = quote.CreatedAt,
                CreatedBy = quote.CreatedBy
            };
        }

        public async Task<QuoteHeaderDto?> UpdateAsync(int quoteId, byte revision, UpdateQuoteHeaderDto dto, string modifiedBy)
        {
            var quote = await _context.QuoteHeaders
                .FirstOrDefaultAsync(q => q.QuoteID == quoteId && q.QuoteRevision == revision);

            if (quote == null)
                return null;

            quote.QuoteName = dto.QuoteName;
            quote.CustomerName = dto.CustomerName;
            quote.Status = dto.Status;
            quote.Currency = dto.Currency;
            quote.ValidityDays = dto.ValidityDays;
            quote.Notes = dto.Notes;
            quote.ModifiedAt = DateTime.UtcNow;
            quote.ModifiedBy = modifiedBy;

            await _context.SaveChangesAsync();

            return new QuoteHeaderDto
            {
                QuoteID = quote.QuoteID,
                QuoteRevision = quote.QuoteRevision,
                QuoteNumber = quote.QuoteNumber,
                QuoteName = quote.QuoteName,
                CustomerName = quote.CustomerName,
                Status = quote.Status,
                Currency = quote.Currency,
                ValidityDays = quote.ValidityDays,
                Notes = quote.Notes,
                CreatedAt = quote.CreatedAt,
                CreatedBy = quote.CreatedBy
            };
        }

        public async Task<bool> DeleteAsync(int quoteId, byte revision)
        {
            var quote = await _context.QuoteHeaders
                .FirstOrDefaultAsync(q => q.QuoteID == quoteId && q.QuoteRevision == revision);

            if (quote == null)
                return false;

            _context.QuoteHeaders.Remove(quote);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<QuoteHeaderDto?> CreateRevisionAsync(int quoteId, byte currentRevision, string createdBy)
        {
            var existingQuote = await _context.QuoteHeaders
                .Include(q => q.QuoteVerticals)
                .FirstOrDefaultAsync(q => q.QuoteID == quoteId && q.QuoteRevision == currentRevision);

            if (existingQuote == null)
                return null;

            var newRevision = (byte)(currentRevision + 1);

            var newQuote = new QuoteHeader
            {
                QuoteID = quoteId,
                QuoteRevision = newRevision,
                QuoteNumber = existingQuote.QuoteNumber,
                QuoteName = existingQuote.QuoteName,
                CustomerName = existingQuote.CustomerName,
                Status = "Draft",
                Currency = existingQuote.Currency,
                ValidityDays = existingQuote.ValidityDays,
                Notes = existingQuote.Notes,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            _context.QuoteHeaders.Add(newQuote);
            await _context.SaveChangesAsync();

            return new QuoteHeaderDto
            {
                QuoteID = newQuote.QuoteID,
                QuoteRevision = newQuote.QuoteRevision,
                QuoteNumber = newQuote.QuoteNumber,
                QuoteName = newQuote.QuoteName,
                CustomerName = newQuote.CustomerName,
                Status = newQuote.Status,
                Currency = newQuote.Currency,
                ValidityDays = newQuote.ValidityDays,
                Notes = newQuote.Notes,
                CreatedAt = newQuote.CreatedAt,
                CreatedBy = newQuote.CreatedBy
            };
        }

        public async Task<List<QuoteHeaderDto>> GetQuotesByCustomerAsync(string customerName)
        {
            return await _context.QuoteHeaders
                .Where(q => q.CustomerName.ToLower().Contains(customerName.ToLower()))
                .OrderByDescending(q => q.CreatedAt)
                .Select(q => new QuoteHeaderDto
                {
                    QuoteID = q.QuoteID,
                    QuoteRevision = q.QuoteRevision,
                    QuoteNumber = q.QuoteNumber,
                    QuoteName = q.QuoteName,
                    CustomerName = q.CustomerName,
                    Status = q.Status,
                    Currency = q.Currency,
                    ValidityDays = q.ValidityDays,
                    CreatedAt = q.CreatedAt,
                    CreatedBy = q.CreatedBy
                })
                .ToListAsync();
        }

        public async Task<List<QuoteHeaderDto>> GetQuotesByStatusAsync(string status)
        {
            return await _context.QuoteHeaders
                .Where(q => q.Status.ToLower() == status.ToLower())
                .OrderByDescending(q => q.CreatedAt)
                .Select(q => new QuoteHeaderDto
                {
                    QuoteID = q.QuoteID,
                    QuoteRevision = q.QuoteRevision,
                    QuoteNumber = q.QuoteNumber,
                    QuoteName = q.QuoteName,
                    CustomerName = q.CustomerName,
                    Status = q.Status,
                    Currency = q.Currency,
                    ValidityDays = q.ValidityDays,
                    CreatedAt = q.CreatedAt,
                    CreatedBy = q.CreatedBy
                })
                .ToListAsync();
        }
    }

}
