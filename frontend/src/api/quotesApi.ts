import type { PaginationParams, PagedResponse, QuoteHeaderDto, CreateQuoteHeaderDto } from "../Dtos/QuoteDto";

 

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5228/api';

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
    },

    async create(dto: CreateQuoteHeaderDto): Promise<QuoteHeaderDto> {
        const response = await fetch(`${API_BASE_URL}/quotes`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(dto),
        });

        if (!response.ok) {
            const errorData = await response.json().catch(() => ({ message: 'Failed to create quote' }));
            throw new Error(errorData.message || 'Failed to create quote');
        }

        return response.json();
    }
};