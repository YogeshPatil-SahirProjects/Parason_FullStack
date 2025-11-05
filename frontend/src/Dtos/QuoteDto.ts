export interface QuoteVerticalDto {
    // Add properties as per your backend DTO
    verticalID: number;
    VerticalName: string;
}

export interface QuoteHeaderDto {
    quoteID: number;
    quoteRevision: number;
    quoteNumber: string;
    quoteName: string;
    customerName: string;
    status: string;
    currency: string;
    validityDays: number;
    notes?: string;
    createdAt: string;
    createdBy: string;
    quoteVerticals?: QuoteVerticalDto[];
}

export interface PagedResponse<T> {
    items: T[];
    totalCount: number;
    pageNumber: number;
    pageSize: number;
    totalPages: number;
    hasPrevious: boolean;
    hasNext: boolean;
}

export interface PaginationParams {
    pageNumber: number;
    pageSize: number;
    searchTerm?: string;
    sortBy?: string;
    sortDescending: boolean;
}

export interface CreateQuoteHeaderDto {
    quoteName: string;
    customerName: string;
    status: string;
    currency: string;
    validityDays: number;
    notes?: string;
}