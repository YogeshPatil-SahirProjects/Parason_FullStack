import React, { useState, useEffect, useCallback } from "react";
import {
    Search,
    ChevronLeft,
    ChevronRight,
    ArrowUpDown,
    ArrowUp,
    ArrowDown,
    FileText,
    Edit,
    Trash2,
    Settings,
} from "lucide-react";
import type { QuoteHeaderDto } from "../Dtos/QuoteDto";
import { quotesApi } from "../api/quotesApi";
import QuotesVertical from "./QuotesVertical";

type SortableColumn =
    | "QuoteNumber"
    | "QuoteName"
    | "CustomerName"
    | "Status"
    | "ValidityDays"
    | "CreatedAt"
    | "CreatedBy";

const QuotesTable: React.FC = () => {
    const [quotes, setQuotes] = useState<QuoteHeaderDto[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);
    const [selectedQuote, setSelectedQuote] = useState<QuoteHeaderDto | null>(
        null
    );

    // pagination + sorting + filtering
    const [pageNumber, setPageNumber] = useState<number>(1);
    const [pageSize, setPageSize] = useState<number>(10);
    const [totalCount, setTotalCount] = useState<number>(0);
    const [searchTerm, setSearchTerm] = useState<string>("");
    const [sortBy, setSortBy] = useState<SortableColumn | "">("");
    const [sortDescending, setSortDescending] = useState<boolean>(false);
    const [searchInput, setSearchInput] = useState<string>("");

    useEffect(() => {
        fetchQuotes();
    }, [pageNumber, pageSize, searchTerm, sortBy, sortDescending]);

    const fetchQuotes = useCallback(async () => {
        setLoading(true);
        setError(null);
        try {
            const data = await quotesApi.getAll({
                pageNumber,
                pageSize,
                searchTerm: searchTerm || undefined,
                sortBy: sortBy || undefined,
                sortDescending,
            });

            setQuotes(data.items);
            setTotalCount(data.totalCount);

            // ✅ Auto-select first quote if not already selected
            if (data.items.length > 0 && !selectedQuote) {
                setSelectedQuote(data.items[0]);
            }
        } catch (err) {
            const message = err instanceof Error ? err.message : "An error occurred";
            setError(message);
            console.error("Error fetching quotes:", err);
        } finally {
            setLoading(false);
        }
    }, [pageNumber, pageSize, searchTerm, sortBy, sortDescending]); // Add dependencies here

    const handleSearch = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setSearchTerm(searchInput);
        setPageNumber(1);
    };

    const handleSort = (column: SortableColumn) => {
        if (sortBy === column) {
            setSortDescending(!sortDescending);
        } else {
            setSortBy(column);
            setSortDescending(false);
        }
        setPageNumber(1);
    };

    const getSortIcon = (column: SortableColumn) => {
        if (sortBy !== column)
            return <ArrowUpDown className="w-4 h-4 text-gray-400" />;
        return sortDescending ? (
            <ArrowDown className="w-4 h-4 text-blue-600" />
        ) : (
            <ArrowUp className="w-4 h-4 text-blue-600" />
        );
    };

    const getStatusBadgeClass = (status: string): string => {
        const base = "px-2 py-1 text-xs font-medium rounded-full";
        switch (status?.toLowerCase()) {
            case "draft":
                return `${base} bg-gray-100 text-gray-700`;
            case "quoted":
                return `${base} bg-green-100 text-green-700`;
            case "won":
                return `${base} bg-blue-100 text-blue-700`;
            case "lost":
                return `${base} bg-red-100 text-red-700`;
            default:
                return `${base} bg-gray-100 text-gray-700`;
        }
    };

    const formatDate = (date: string) =>
        new Date(date).toLocaleDateString("en-US", {
            year: "numeric",
            month: "short",
            day: "numeric",
        });

    const totalPages = Math.ceil(totalCount / pageSize);

    if (error) {
        return (
            <div className="bg-red-50 border border-red-200 rounded-lg p-4">
                <p className="text-red-800">Error: {error}</p>
            </div>
        );
    }

    return (
        <>
            <div className="bg-white rounded-lg shadow-sm border border-gray-200"> 
                {/* Header */}
                <div className="px-6 py-2 border-b border-gray-200 flex items-center justify-between">
                    {/* Title inline */}
                    <div className="flex items-center space-x-3">
                        <h2 className="text-lg font-semibold text-gray-800">Quotes</h2>
                        <p className="text-sm text-gray-500">Manage and view all quotes</p>
                    </div>

                    {/* Search Bar */}
                    <form onSubmit={handleSearch} className="flex items-center space-x-2">
                        <div className="relative">
                            <input
                                type="text"
                                value={searchInput}
                                onChange={(e) => setSearchInput(e.target.value)}
                                placeholder="Search quotes..."
                                className="pl-8 pr-3 py-1.5 border border-gray-300 rounded-md focus:ring-1 focus:ring-blue-500 focus:border-blue-500 w-56 text-sm"
                            />
                            <Search className="w-4 h-4 text-gray-400 absolute left-2.5 top-1/2 transform -translate-y-1/2" />
                        </div>
                        <button
                            type="submit"
                            className="px-3 py-1.5 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors text-sm font-medium"
                        >
                            Search
                        </button>
                    </form>
                </div>


                {/* Table */}
                <div className="overflow-x-auto">
                    <table className="w-full">
                        <thead className="bg-gray-50 border-b border-gray-200">
                            <tr>
                                {[
                                    ["QuoteNumber", "Quote #"],
                                    ["QuoteName", "Quote Name"],
                                    ["CustomerName", "Customer"],
                                    ["Status", "Status"],
                                    ["ValidityDays", "Validity"],
                                    ["CreatedAt", "Created"],
                                    ["CreatedBy", "Created By"],
                                ].map(([key, label]) => (
                                    <th key={key} className="px-6 py-3 text-left">
                                        <button
                                            onClick={() => handleSort(key as SortableColumn)}
                                            className="flex items-center space-x-1 text-xs font-semibold text-gray-700 uppercase tracking-wider hover:text-blue-600"
                                        >
                                            <span>{label}</span>
                                            {getSortIcon(key as SortableColumn)}
                                        </button>
                                    </th>
                                ))}
                                <th className="px-6 py-3 text-left text-xs font-semibold text-gray-700 uppercase tracking-wider">
                                    Currency
                                </th>
                                <th className="px-6 py-3 text-left text-xs font-semibold text-gray-700 uppercase tracking-wider">
                                    Actions
                                </th>
                            </tr>
                        </thead>

                        <tbody className="divide-y divide-gray-200 text-sm text-gray-700">
                            {loading ? (
                                <tr>
                                    <td colSpan={9} className="text-center py-8">
                                        <div className="flex justify-center">
                                            <div className="animate-spin h-8 w-8 border-b-2 border-blue-600 rounded-full"></div>
                                        </div>
                                    </td>
                                </tr>
                            ) : quotes.length === 0 ? (
                                <tr>
                                    <td colSpan={9} className="text-center py-8 text-gray-500">
                                        <FileText className="w-12 h-12 mx-auto text-gray-300" />
                                        No quotes found
                                    </td>
                                </tr>
                            ) : (
                                quotes.map((quote) => (
                                    <tr
                                        key={`${quote.quoteID}-${quote.quoteRevision}`}
                                        className={`cursor-pointer transition-colors hover:bg-gray-50 ${selectedQuote?.quoteID === quote.quoteID
                                            ? "bg-blue-50"
                                            : ""
                                            }`}
                                        onClick={() => setSelectedQuote(quote)}
                                    >
                                        <td className="px-6 py-3 text-blue-600 font-medium whitespace-nowrap">
                                            {quote.quoteNumber}
                                        </td>

                                        <td className="px-6 py-3 whitespace-nowrap overflow-hidden text-ellipsis max-w-[180px]" title={quote.quoteName}>
                                            {quote.quoteName}
                                        </td>

                                        <td className="px-6 py-3 whitespace-nowrap overflow-hidden text-ellipsis max-w-[180px]" title={quote.customerName}>
                                            {quote.customerName}
                                        </td>

                                        <td className="px-6 py-3 whitespace-nowrap">
                                            <span className={getStatusBadgeClass(quote.status)}>
                                                {quote.status}
                                            </span>
                                        </td>

                                        <td className="px-6 py-3 whitespace-nowrap">{quote.validityDays} days</td>
                                        <td className="px-6 py-3 whitespace-nowrap">{formatDate(quote.createdAt)}</td>

                                        <td className="px-6 py-3 whitespace-nowrap overflow-hidden text-ellipsis max-w-[140px]" title={quote.createdBy}>
                                            {quote.createdBy}
                                        </td>

                                        <td className="px-6 py-3 whitespace-nowrap">{quote.currency}</td>

                                        <td className="px-6 py-3 whitespace-nowrap">
                                            <div className="flex items-center space-x-2">
                                                <button
                                                    onClick={(e) => {
                                                        e.stopPropagation();
                                                        console.log("Edit quote:", quote);
                                                    }}
                                                    className="p-2 text-blue-600 hover:bg-blue-50 rounded-md"
                                                    title="Edit"
                                                >
                                                    <Edit className="w-4 h-4" />
                                                </button>
                                                <button
                                                    onClick={(e) => {
                                                        e.stopPropagation();
                                                        console.log("Configure:", quote);
                                                    }}
                                                    className="p-2 text-gray-600 hover:bg-gray-50 rounded-md"
                                                    title="Configure Vertical"
                                                >
                                                    <Settings className="w-4 h-4" />
                                                </button>
                                                <button
                                                    onClick={(e) => {
                                                        e.stopPropagation();
                                                        console.log("Delete:", quote);
                                                    }}
                                                    className="p-2 text-red-600 hover:bg-red-50 rounded-md"
                                                    title="Delete"
                                                >
                                                    <Trash2 className="w-4 h-4" />
                                                </button>
                                            </div>
                                        </td>
                                    </tr>
                                ))
                            )}
                        </tbody>
                    </table>
                </div>

                {/* Pagination */}
                {totalCount > 0 && (
                    <div className="px-6 py-4 border-t border-gray-200 flex items-center justify-between">
                        <div className="flex items-center space-x-2 text-sm text-gray-700">
                            <span>Rows per page:</span>
                            <select
                                value={pageSize}
                                onChange={(e) => {
                                    setPageSize(Number(e.target.value));
                                    setPageNumber(1);
                                }}
                                className="border border-gray-300 rounded px-2 py-1 text-sm"
                            >
                                {[5, 10, 25, 50].map((size) => (
                                    <option key={size} value={size}>
                                        {size}
                                    </option>
                                ))}
                            </select>
                            <span>
                                Showing {(pageNumber - 1) * pageSize + 1}–
                                {Math.min(pageNumber * pageSize, totalCount)} of {totalCount}
                            </span>
                        </div>

                        <div className="flex items-center space-x-2">
                            <button
                                onClick={() => setPageNumber(Math.max(1, pageNumber - 1))}
                                disabled={pageNumber === 1}
                                className="p-2 border border-gray-300 rounded disabled:opacity-50"
                            >
                                <ChevronLeft className="w-4 h-4" />
                            </button>
                            <span className="text-sm text-gray-600">
                                {pageNumber} / {totalPages}
                            </span>
                            <button
                                onClick={() => setPageNumber(Math.min(totalPages, pageNumber + 1))}
                                disabled={pageNumber === totalPages}
                                className="p-2 border border-gray-300 rounded disabled:opacity-50"
                            >
                                <ChevronRight className="w-4 h-4" />
                            </button>
                        </div>
                    </div>
                )}
            </div>

            {/* ✅ Show first vertical of selected quote */}
            {/* ✅ Show first vertical of selected quote or message if no verticals */}
            {selectedQuote && (
                <div className="mt-2">
                    {selectedQuote.quoteVerticals && selectedQuote.quoteVerticals.length > 0 ? (
                        <QuotesVertical
                            quoteId={selectedQuote.quoteID}
                            quoteRevision={selectedQuote.quoteRevision}
                            verticalId={selectedQuote.quoteVerticals[0].verticalID}
                        />
                    ) : (
                        <div className="bg-white rounded-lg shadow-sm border border-gray-200 p-8 text-center">
                            <Settings className="w-12 h-12 mx-auto text-gray-300 mb-3" />
                            <p className="text-gray-600 font-medium">No Verticals Configured</p>
                            <p className="text-gray-500 text-sm mt-1">
                                Verticals have not been configured for this quotation.
                            </p>
                        </div>
                    )}
                </div>
            )}
        </>
    );
};

export default QuotesTable;