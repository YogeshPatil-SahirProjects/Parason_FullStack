import type { PaginationParams, PagedResponse, QuoteHeaderDto } from "../Dtos/QuoteDto";

 

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'https://localhost:7008/api';

export const quotesApi = {
    async getAll(params: PaginationParams): Promise<PagedResponse<QuoteHeaderDto>> {
        const queryParams = new URLSearchParams({
            PageNumber: params.pageNumber.toString(),
            PageSize: params.pageSize.toString(),
            SortDescending: params.sortDescending.toString(),
            ...(params.searchTerm && { SearchTerm: params.searchTerm }),
            ...(params.sortBy && { SortBy: params.sortBy }),
        });

        const response = await fetch(`${API_BASE_URL}/quotes?${queryParams}`);

        if (!response.ok) {
            throw new Error('Failed to fetch quotes');
        }

        return response.json();
    }
};