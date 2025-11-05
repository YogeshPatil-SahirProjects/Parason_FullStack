namespace Parason_Api.DTOs;

public class QuoteSearchDto
{
    // Pagination
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    // Search filters
    public string? SearchTerm { get; set; }
    public string? QuoteNumber { get; set; }
    public string? QuoteName { get; set; }
    public string? CustomerName { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }

    // Sorting
    public string SortBy { get; set; } = "CreatedAt";
    public string SortOrder { get; set; } = "desc"; // asc or desc
}

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}
