import React, { useState } from "react";
import { Save, X, AlertCircle, CheckCircle } from "lucide-react";
import type { CreateQuoteHeaderDto } from "../Dtos/QuoteDto";
import { quotesApi } from "../api/quotesApi";

interface QuoteFormProps {
    onNavigate: (page: 'home' | 'about' | 'createQuote') => void;
}

interface FormErrors {
    quoteName?: string;
    customerName?: string;
    status?: string;
    currency?: string;
    validityDays?: string;
}

const QuoteForm: React.FC<QuoteFormProps> = ({ onNavigate }) => {
    const [formData, setFormData] = useState<CreateQuoteHeaderDto>({
        quoteName: "",
        customerName: "",
        status: "Draft",
        currency: "USD",
        validityDays: 30,
        notes: "",
    });

    const [errors, setErrors] = useState<FormErrors>({});
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [submitError, setSubmitError] = useState<string | null>(null);
    const [submitSuccess, setSubmitSuccess] = useState(false);

    const statusOptions = ["Draft", "Quoted", "Won", "Lost"];
    const currencyOptions = ["USD", "EUR", "GBP", "INR", "AUD", "CAD", "JPY"];

    const validateForm = (): boolean => {
        const newErrors: FormErrors = {};

        if (!formData.quoteName.trim()) {
            newErrors.quoteName = "Quote name is required";
        }

        if (!formData.customerName.trim()) {
            newErrors.customerName = "Customer name is required";
        }

        if (!formData.status) {
            newErrors.status = "Status is required";
        }

        if (!formData.currency) {
            newErrors.currency = "Currency is required";
        }

        if (formData.validityDays <= 0) {
            newErrors.validityDays = "Validity days must be greater than 0";
        }

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setSubmitError(null);
        setSubmitSuccess(false);

        if (!validateForm()) {
            return;
        }

        setIsSubmitting(true);

        try {
            await quotesApi.create(formData);
            setSubmitSuccess(true);

            // Redirect after 1.5 seconds
            setTimeout(() => {
                onNavigate('home');
            }, 1500);
        } catch (error) {
            const message = error instanceof Error ? error.message : "An error occurred while creating the quote";
            setSubmitError(message);
            console.error("Error creating quote:", error);
        } finally {
            setIsSubmitting(false);
        }
    };

    const handleInputChange = (
        e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>
    ) => {
        const { name, value } = e.target;
        setFormData((prev) => ({
            ...prev,
            [name]: name === "validityDays" ? Number(value) : value,
        }));

        // Clear error for this field when user starts typing
        if (errors[name as keyof FormErrors]) {
            setErrors((prev) => ({
                ...prev,
                [name]: undefined,
            }));
        }
    };

    const handleCancel = () => {
        onNavigate('home');
    };

    return (
        <div className="min-h-screen bg-gray-50 py-8">
            <div className="container mx-auto px-4 max-w-3xl">
                <div className="bg-white rounded-lg shadow-sm border border-gray-200">
                    {/* Header */}
                    <div className="px-6 py-4 border-b border-gray-200">
                        <h2 className="text-2xl font-semibold text-gray-800">Create New Quote</h2>
                        <p className="text-sm text-gray-500 mt-1">Fill in the details to create a new quote</p>
                    </div>

                    {/* Success Message */}
                    {submitSuccess && (
                        <div className="mx-6 mt-6 bg-green-50 border border-green-200 rounded-lg p-4 flex items-center space-x-3">
                            <CheckCircle className="w-5 h-5 text-green-600" />
                            <p className="text-green-800 font-medium">Quote created successfully! Redirecting...</p>
                        </div>
                    )}

                    {/* Error Message */}
                    {submitError && (
                        <div className="mx-6 mt-6 bg-red-50 border border-red-200 rounded-lg p-4 flex items-center space-x-3">
                            <AlertCircle className="w-5 h-5 text-red-600" />
                            <p className="text-red-800">{submitError}</p>
                        </div>
                    )}

                    {/* Form */}
                    <form onSubmit={handleSubmit} className="p-6">
                        <div className="space-y-6">
                            {/* Quote Name */}
                            <div>
                                <label htmlFor="quoteName" className="block text-sm font-medium text-gray-700 mb-1">
                                    Quote Name <span className="text-red-500">*</span>
                                </label>
                                <input
                                    type="text"
                                    id="quoteName"
                                    name="quoteName"
                                    value={formData.quoteName}
                                    onChange={handleInputChange}
                                    className={`w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500 focus:border-blue-500 ${
                                        errors.quoteName ? "border-red-500" : "border-gray-300"
                                    }`}
                                    placeholder="Enter quote name"
                                />
                                {errors.quoteName && (
                                    <p className="mt-1 text-sm text-red-600">{errors.quoteName}</p>
                                )}
                            </div>

                            {/* Customer Name */}
                            <div>
                                <label htmlFor="customerName" className="block text-sm font-medium text-gray-700 mb-1">
                                    Customer Name <span className="text-red-500">*</span>
                                </label>
                                <input
                                    type="text"
                                    id="customerName"
                                    name="customerName"
                                    value={formData.customerName}
                                    onChange={handleInputChange}
                                    className={`w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500 focus:border-blue-500 ${
                                        errors.customerName ? "border-red-500" : "border-gray-300"
                                    }`}
                                    placeholder="Enter customer name"
                                />
                                {errors.customerName && (
                                    <p className="mt-1 text-sm text-red-600">{errors.customerName}</p>
                                )}
                            </div>

                            {/* Status and Currency Row */}
                            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                                {/* Status */}
                                <div>
                                    <label htmlFor="status" className="block text-sm font-medium text-gray-700 mb-1">
                                        Status <span className="text-red-500">*</span>
                                    </label>
                                    <select
                                        id="status"
                                        name="status"
                                        value={formData.status}
                                        onChange={handleInputChange}
                                        className={`w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500 focus:border-blue-500 ${
                                            errors.status ? "border-red-500" : "border-gray-300"
                                        }`}
                                    >
                                        {statusOptions.map((option) => (
                                            <option key={option} value={option}>
                                                {option}
                                            </option>
                                        ))}
                                    </select>
                                    {errors.status && (
                                        <p className="mt-1 text-sm text-red-600">{errors.status}</p>
                                    )}
                                </div>

                                {/* Currency */}
                                <div>
                                    <label htmlFor="currency" className="block text-sm font-medium text-gray-700 mb-1">
                                        Currency <span className="text-red-500">*</span>
                                    </label>
                                    <select
                                        id="currency"
                                        name="currency"
                                        value={formData.currency}
                                        onChange={handleInputChange}
                                        className={`w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500 focus:border-blue-500 ${
                                            errors.currency ? "border-red-500" : "border-gray-300"
                                        }`}
                                    >
                                        {currencyOptions.map((option) => (
                                            <option key={option} value={option}>
                                                {option}
                                            </option>
                                        ))}
                                    </select>
                                    {errors.currency && (
                                        <p className="mt-1 text-sm text-red-600">{errors.currency}</p>
                                    )}
                                </div>
                            </div>

                            {/* Validity Days */}
                            <div>
                                <label htmlFor="validityDays" className="block text-sm font-medium text-gray-700 mb-1">
                                    Validity Days <span className="text-red-500">*</span>
                                </label>
                                <input
                                    type="number"
                                    id="validityDays"
                                    name="validityDays"
                                    value={formData.validityDays}
                                    onChange={handleInputChange}
                                    min="1"
                                    className={`w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500 focus:border-blue-500 ${
                                        errors.validityDays ? "border-red-500" : "border-gray-300"
                                    }`}
                                    placeholder="Enter validity days"
                                />
                                {errors.validityDays && (
                                    <p className="mt-1 text-sm text-red-600">{errors.validityDays}</p>
                                )}
                            </div>

                            {/* Notes */}
                            <div>
                                <label htmlFor="notes" className="block text-sm font-medium text-gray-700 mb-1">
                                    Notes
                                </label>
                                <textarea
                                    id="notes"
                                    name="notes"
                                    value={formData.notes}
                                    onChange={handleInputChange}
                                    rows={4}
                                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                                    placeholder="Enter any additional notes (optional)"
                                />
                            </div>
                        </div>

                        {/* Form Actions */}
                        <div className="mt-8 flex items-center justify-end space-x-3 pt-6 border-t border-gray-200">
                            <button
                                type="button"
                                onClick={handleCancel}
                                disabled={isSubmitting}
                                className="px-4 py-2 border border-gray-300 text-gray-700 rounded-md hover:bg-gray-50 transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center space-x-2"
                            >
                                <X className="w-4 h-4" />
                                <span>Cancel</span>
                            </button>
                            <button
                                type="submit"
                                disabled={isSubmitting || submitSuccess}
                                className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center space-x-2"
                            >
                                {isSubmitting ? (
                                    <>
                                        <div className="animate-spin h-4 w-4 border-b-2 border-white rounded-full"></div>
                                        <span>Creating...</span>
                                    </>
                                ) : (
                                    <>
                                        <Save className="w-4 h-4" />
                                        <span>Save Quote</span>
                                    </>
                                )}
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default QuoteForm;
