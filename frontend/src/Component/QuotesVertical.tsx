import React, { useEffect, useState } from "react";
import {
    Layers,
    Wrench,
    Workflow,
    Boxes,
    Package,
    ChevronRight,
    ChevronDown,
    Info,
    ClipboardList,
    IndianRupee,
} from "lucide-react";

/* ------------------- DTO Interfaces ------------------- */
interface ModelDto {
    modelID: number;
    seriesID: number;
    modelCode: string;
    modelName: string;
    description?: string;
    isActive: boolean;
    seriesName?: string;
    basePrice?: number | null;
    quantity?: number;
}

interface SeriesDto {
    seriesID: number;
    seriesCode: string;
    seriesName: string;
    description?: string;
    isActive: boolean;
    models: ModelDto[];
}

interface EquipmentDto {
    equipmentID: number;
    equipmentCode: string;
    equipmentName: string;
    description?: string;
    isActive: boolean;
    series: SeriesDto[];
}

export interface ScopeOfSupplyDto {
    recordID: number;
    modelID: number;
    itemId: number;
    price_INR: number;
    quantity: number;
    itemName: string;
    itemCode: string;
    description?: string;
    isMandatory: boolean;
    modelName?: string;
}

export interface SpecDetailsDto {
    recordID: number;
    equipmentID: number;
    modelID: number;
    attributeID: number;
    listValueID: number;
    numValue: number;
    textValue: string;
    boolValue: boolean;
    attributeName: string;
    dataType: string;
    listValueDisplay: string;
}

interface ProcessDto {
    processID: number;
    processCode: string;
    processName: string;
    description?: string;
    isActive: boolean;
    equipments: EquipmentDto[];
    scopeItems: ScopeOfSupplyDto[];
    specifications: SpecDetailsDto[];
}

interface QuoteVerticalDto {
    verticalID: number;
    verticalName: string;
    layer: string;
    total_Price: number;
    processes: ProcessDto[];
}

interface QuotesVerticalProps {
    quoteId: number;
    quoteRevision: number;
    verticalId: number;
}

type TabType = "equipment" | "scope";

/* ------------------- Component ------------------- */
const QuotesVerticalImproved: React.FC<QuotesVerticalProps> = ({
    quoteId,
    quoteRevision,
    verticalId,
}) => {
    const [vertical, setVertical] = useState<QuoteVerticalDto | null>(null);
    const [selectedProcess, setSelectedProcess] = useState<ProcessDto | null>(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [activeTab, setActiveTab] = useState<TabType>("equipment");
    const [expandedEquipment, setExpandedEquipment] = useState<Set<number>>(new Set());
    const [expandedSeries, setExpandedSeries] = useState<Set<number>>(new Set());

    const formatToLakhs = (value: number): string => {
        if (isNaN(value)) return "-";
        if (value >= 10000000) return `₹${(value / 10000000).toFixed(2)} Cr`;
        else if (value >= 100000) return `₹${(value / 100000).toFixed(2)} L`;
        else if (value >= 10000) return `₹${(value / 10000).toFixed(2)} K`;
        else return `₹${value.toLocaleString("en-IN")}`;
    };

    useEffect(() => {
        if (quoteId) fetchVerticalDetails(quoteId, quoteRevision, verticalId);
    }, [quoteId, quoteRevision, verticalId]);

    useEffect(() => {
        // Auto-expand first equipment and series when process changes
        if (selectedProcess?.equipments && selectedProcess.equipments.length > 0) {
            const firstEquipment = selectedProcess.equipments[0];
            setExpandedEquipment(new Set([firstEquipment.equipmentID]));
            if (firstEquipment?.series && firstEquipment.series.length > 0) {
                setExpandedSeries(new Set([firstEquipment.series[0].seriesID]));
            }
        }
    }, [selectedProcess]);

    const fetchVerticalDetails = async (
        quoteId: number,
        quoteRevision: number,
        verticalId: number
    ) => {
        setLoading(true);
        setError(null);
        try {
            const response = await fetch(
                `http://localhost:5228/api/QuoteVertical/VerticalConfig/${quoteId}/${quoteRevision}/${verticalId}`
            );
            if (!response.ok) throw new Error("Failed to fetch vertical details");
            const data = await response.json();

            // Ensure arrays have default values if undefined
            if (data) {
                data.processes = data.processes || [];
                data.processes.forEach((process: ProcessDto) => {
                    process.equipments = process.equipments || [];
                    process.scopeItems = process.scopeItems || [];
                    process.specifications = process.specifications || [];
                    process.equipments.forEach((equipment: EquipmentDto) => {
                        equipment.series = equipment.series || [];
                        equipment.series.forEach((series: SeriesDto) => {
                            series.models = series.models || [];
                        });
                    });
                });
            }

            setVertical(data);
            if (data?.processes?.length > 0) setSelectedProcess(data.processes[0]);
        } catch (err) {
            setError(err instanceof Error ? err.message : "An error occurred");
        } finally {
            setLoading(false);
        }
    };

    const toggleEquipment = (equipmentID: number) => {
        setExpandedEquipment((prev) => {
            const newSet = new Set(prev);
            if (newSet.has(equipmentID)) {
                newSet.delete(equipmentID);
            } else {
                newSet.add(equipmentID);
            }
            return newSet;
        });
    };

    const toggleSeries = (seriesID: number) => {
        setExpandedSeries((prev) => {
            const newSet = new Set(prev);
            if (newSet.has(seriesID)) {
                newSet.delete(seriesID);
            } else {
                newSet.add(seriesID);
            }
            return newSet;
        });
    };

    const CodeBadge = ({ text, color }: { text: string; color: string }) => (
        <span className={`ml-2 px-2 py-0.5 text-xs rounded-full font-medium text-white ${color}`}>
            {text}
        </span>
    );

    /* ------------------- TAB CONTENT RENDERERS ------------------- */
    const renderEquipmentTab = (process: ProcessDto) => {
        const equipments = process.equipments || [];

        if (equipments.length === 0) {
            return (
                <div className="text-center py-8 text-gray-500">
                    No equipment configured for this process
                </div>
            );
        }

        const totalModels = equipments.reduce(
            (sum, eq) => sum + (eq.series || []).reduce((s, ser) => s + (ser.models || []).length, 0),
            0
        );

        return (
            <div className="space-y-3">
                <div className="flex items-center justify-between text-sm text-gray-600 pb-2 border-b">
                    <span>
                        <strong>{equipments.length}</strong> Equipment •{" "}
                        <strong>{totalModels}</strong> Models
                    </span>
                </div>

                {equipments.map((equipment, equipmentIndex) => {
                    const isExpanded = expandedEquipment.has(equipment.equipmentID);
                    const series = equipment.series || [];
                    const modelCount = series.reduce(
                        (sum, s) => sum + (s.models || []).length,
                        0
                    );

                    return (
                        <div
                            key={`process-${process.processID}-equipment-${equipment.equipmentID}-idx-${equipmentIndex}`}
                            className="border border-gray-200 rounded-lg overflow-hidden hover:border-blue-300 transition-colors"
                        >
                            {/* Equipment Header */}
                            <button
                                onClick={() => toggleEquipment(equipment.equipmentID)}
                                className="w-full flex items-center justify-between p-3 bg-gray-50 hover:bg-gray-100 transition-colors"
                            >
                                <div className="flex items-center gap-2">
                                    {isExpanded ? (
                                        <ChevronDown className="w-4 h-4 text-gray-500" />
                                    ) : (
                                        <ChevronRight className="w-4 h-4 text-gray-500" />
                                    )}
                                    <Wrench className="w-4 h-4 text-blue-600" />
                                    <span className="font-semibold text-gray-800">
                                        {equipment.equipmentName}
                                    </span>
                                    <CodeBadge text={equipment.equipmentCode} color="bg-blue-600" />
                                </div>
                                <span className="text-xs text-gray-500">
                                    {series.length} Series • {modelCount} Models
                                </span>
                            </button>

                            {/* Equipment Content */}
                            {isExpanded && (
                                <div className="p-2 space-y-2 bg-white">
                                    {equipment.description && (
                                        <p className="text-xs text-gray-600 pb-2 border-b">
                                            {equipment.description}
                                        </p>
                                    )}

                                    {series.map((seriesItem, seriesIndex) => {
                                        const isSeriesExpanded = expandedSeries.has(seriesItem.seriesID);
                                        const models = seriesItem.models || [];

                                        return (
                                            <div
                                                key={`equipment-${equipment.equipmentID}-series-${seriesItem.seriesID}-idx-${seriesIndex}`}
                                                className="border border-gray-100 rounded overflow-hidden"
                                            >
                                                {/* Series Header */}
                                                <button
                                                    onClick={() => toggleSeries(seriesItem.seriesID)}
                                                    className="w-full flex items-center justify-between p-2 bg-purple-50 hover:bg-purple-100 transition-colors"
                                                >
                                                    <div className="flex items-center gap-2">
                                                        {isSeriesExpanded ? (
                                                            <ChevronDown className="w-3 h-3 text-gray-500" />
                                                        ) : (
                                                            <ChevronRight className="w-3 h-3 text-gray-500" />
                                                        )}
                                                        <Boxes className="w-3 h-3 text-purple-500" />
                                                        <span className="text-sm font-medium text-gray-800">
                                                            {seriesItem.seriesName}
                                                        </span>
                                                        <CodeBadge
                                                            text={seriesItem.seriesCode}
                                                            color="bg-purple-500"
                                                        />
                                                    </div>
                                                    <span className="text-xs text-gray-500">
                                                        {models.length} Models
                                                    </span>
                                                </button>

                                                {/* Models */}
                                                {isSeriesExpanded && (
                                                    <div className="p-2 space-y-1 bg-white">
                                                        {seriesItem.description && (
                                                            <p className="text-xs text-gray-600 mb-2">
                                                                {seriesItem.description}
                                                            </p>
                                                        )}
                                                        {models.map((model, modelIndex) => (
                                                            <div
                                                                key={`series-${seriesItem.seriesID}-model-${model.modelID}-idx-${modelIndex}`}
                                                                className="flex items-start justify-between p-2 bg-indigo-50 rounded hover:bg-indigo-100 transition-colors"
                                                            >
                                                                <div className="flex-1">
                                                                    <div className="flex items-center gap-2">
                                                                        <Package className="w-3 h-3 text-indigo-500 flex-shrink-0" />
                                                                        <span className="text-sm font-medium text-gray-800">
                                                                            {model.modelName}
                                                                        </span>
                                                                        <CodeBadge
                                                                            text={model.modelCode}
                                                                            color="bg-indigo-500"
                                                                        />
                                                                    </div>
                                                                    {model.description && (
                                                                        <p className="text-xs text-gray-600 mt-1 ml-5">
                                                                            {model.description}
                                                                        </p>
                                                                    )}
                                                                </div>
                                                                <div className="text-xs text-gray-700 text-right ml-4 flex-shrink-0">
                                                                    {model.quantity && (
                                                                        <div>
                                                                            <strong>Qty:</strong>{" "}
                                                                            {model.quantity}
                                                                        </div>
                                                                    )}
                                                                    {model.basePrice && (
                                                                        <div className="flex items-center justify-end gap-1">
                                                                            <IndianRupee className="w-3 h-3 text-gray-500" />
                                                                            <span>
                                                                                {model.basePrice.toLocaleString()}
                                                                            </span>
                                                                        </div>
                                                                    )}
                                                                </div>
                                                            </div>
                                                        ))}
                                                    </div>
                                                )}
                                            </div>
                                        );
                                    })}
                                </div>
                            )}
                        </div>
                    );
                })}
            </div>
        );
    };

    const renderScopeTab = (process: ProcessDto) => {
        if (!process.scopeItems?.length) {
            return (
                <div className="text-center py-8 text-gray-500">
                    No scope items available for this process
                </div>
            );
        }

        // Group items by mandatory/optional
        const mandatoryItems = process.scopeItems.filter(item => item.isMandatory);
        const optionalItems = process.scopeItems.filter(item => !item.isMandatory);

        const mandatoryTotal = mandatoryItems.reduce((sum, item) => sum + item.price_INR * item.quantity, 0);
        const optionalTotal = optionalItems.reduce((sum, item) => sum + item.price_INR * item.quantity, 0);
        const totalValue = mandatoryTotal + optionalTotal;

        const renderItemGroup = (
            items: ScopeOfSupplyDto[],
            title: string,
            isMandatory: boolean
        ) => {
            if (!items.length) return null;

            const groupTotal = items.reduce((sum, item) => sum + item.price_INR * item.quantity, 0);
            const headerBgColor = isMandatory ? "bg-green-50" : "bg-yellow-50";
            const headerBorderColor = isMandatory ? "border-green-200" : "border-yellow-200";
            const headerTextColor = isMandatory ? "text-green-700" : "text-yellow-700";
            const badgeBgColor = isMandatory ? "bg-green-100" : "bg-yellow-100";
            const badgeTextColor = isMandatory ? "text-green-700" : "text-yellow-700";
            const itemBgColor = isMandatory ? "bg-green-50" : "bg-yellow-50";
            const itemHoverBgColor = isMandatory ? "hover:bg-green-100" : "hover:bg-yellow-100";

            return (
                <div className={`border ${headerBorderColor} rounded-lg overflow-hidden`}>
                    {/* Group Header */}
                    <div className={`${headerBgColor} px-4 py-3 border-b ${headerBorderColor}`}>
                        <div className="flex items-center justify-between">
                            <div className="flex items-center gap-3">
                                <h4 className={`font-semibold ${headerTextColor} text-base`}>
                                    {title}
                                </h4>
                                <span className={`px-2.5 py-1 ${badgeBgColor} ${badgeTextColor} rounded-full text-xs font-medium`}>
                                    {items.length} {items.length === 1 ? 'Item' : 'Items'}
                                </span>
                            </div>
                            <div className={`font-semibold ${headerTextColor}`}>
                                Subtotal: {formatToLakhs(groupTotal)}
                            </div>
                        </div>
                    </div>

                    {/* Items List */}
                    <div className="divide-y divide-gray-200">
                        {items.map((item, itemIndex) => (
                            <div
                                key={`process-${process.processID}-scope-${item.recordID}-idx-${itemIndex}`}
                                className={`${itemBgColor} ${itemHoverBgColor} p-4 transition-colors`}
                            >
                                <div className="flex items-start justify-between gap-4">
                                    {/* Left: Item Info */}
                                    <div className="flex-1 min-w-0">
                                        <div className="flex items-center gap-2 mb-1">
                                            <div className="flex flex-col">
                                                <div className="flex items-center gap-2 mb-1">
                                                    <h5 className="font-medium text-gray-800 text-sm">
                                                        {item.itemName}
                                                    </h5>
                                                    <span className="px-2 py-0.5 bg-indigo-100 text-indigo-700 rounded text-xs font-medium">
                                                        {item.itemCode}
                                                    </span>
                                                </div>

                                                {item.modelName && (
                                                    <div className="ml-1 text-xs text-gray-500 flex items-center gap-1">
                                                        <Package className="w-3 h-3 text-gray-400" />
                                                        <span>Used for model:</span>
                                                        <span className="font-medium text-gray-700">{item.modelName}</span>
                                                    </div>
                                                )}
                                            </div>

                                        </div>
                                        {item.description && (
                                            <p className="text-xs text-gray-600 mt-1">
                                                {item.description}
                                            </p>
                                        )}
                                    </div>

                                    {/* Right: Pricing Info */}
                                    <div className="flex-shrink-0 text-right">
                                        <div className="grid grid-cols-2 gap-x-4 gap-y-1 text-xs">
                                            <span className="text-gray-600">Unit Price:</span>
                                            <span className="font-medium text-gray-800">
                                                {formatToLakhs(item.price_INR)}
                                            </span>

                                            <span className="text-gray-600">Quantity:</span>
                                            <span className="font-medium text-gray-800">
                                                {item.quantity}
                                            </span>

                                            <span className="text-gray-600 font-semibold">Total:</span>
                                            <span className="font-semibold text-gray-900">
                                                {formatToLakhs(item.price_INR * item.quantity)}
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        ))}
                    </div>
                </div>
            );
        };

        return (
            <div className="space-y-2">
                {/* Overall Summary */}
                <div className="bg-gradient-to-r from-blue-50 to-emerald-50 border border-blue-200 rounded-lg p-2">
                    <div className="flex items-center justify-between">
                        <div className="flex items-center gap-4 text-sm">
                            <div>
                                <span className="text-gray-600">Total Items: </span>
                                <span className="font-semibold text-blue-800">{process.scopeItems.length}</span>
                            </div>
                            <div className="h-4 w-px bg-gray-300"></div>
                            <div>
                                <span className="text-gray-600">Mandatory Items: </span>                               
                                <span className="font-semibold text-blue-700">{mandatoryItems.length}</span>
                            </div>
                            <div className="h-4 w-px bg-gray-300"></div>
                            <div>
                                <span className="text-gray-600">Optional Items: </span>
                                <span className="font-semibold text-blue-700">{optionalItems.length}</span>
                            </div>
                        </div>
                        <div className="text-right">
                            <div className="text-xs text-gray-600 mb-1">Grand Total</div>
                            <div className="text-lg font-bold text-blue-700">
                                {formatToLakhs(totalValue)}
                            </div>
                        </div>
                    </div>
                </div>

                {/* Mandatory Items */}
                {renderItemGroup(mandatoryItems, "Mandatory Items", true)}

                {/* Optional Items */}
                {renderItemGroup(optionalItems, "Optional Items", false)}
            </div>
        );
    };

    /* ------------------- LEFT SIDEBAR ------------------- */
    const renderProcessList = () => {
        const processes = vertical?.processes || [];

        if (processes.length === 0) {
            return (
                <div className="h-full flex flex-col items-center justify-center text-center p-4">
                    <Workflow className="w-12 h-12 text-gray-300 mb-3" />
                    <p className="text-gray-600 font-medium">No Processes Configured</p>
                    <p className="text-gray-500 text-xs mt-1">
                        This vertical does not have any processes configured yet.
                    </p>
                </div>
            );
        }

        return (
            <div className="h-full flex flex-col">
                <div className="flex items-center justify-between mb-4 pb-3 border-b">
                    <h4 className="text-sm font-semibold text-gray-700 flex items-center">
                        <Workflow className="w-4 h-4 text-green-600 mr-1" /> Processes
                    </h4>
                    <span className="text-xs bg-green-100 text-green-700 px-2 py-0.5 rounded-full font-medium">
                        {processes.length}
                    </span>
                </div>

                <div className="flex-1 overflow-y-auto pr-2">
                    <ul className="space-y-2">
                        {processes.map((p, processIndex) => {
                            const selected = selectedProcess?.processID === p.processID;
                            const equipments = p.equipments || [];
                            const scopeItems = p.scopeItems || [];
                            const equipmentCount = equipments.length;
                            const modelCount = equipments.reduce(
                                (sum, eq) =>
                                    sum + (eq.series || []).reduce((s, ser) => s + (ser.models || []).length, 0),
                                0
                            );
                            const itemsCount = scopeItems.length;                       

                        return (
                            <li key={`vertical-${vertical?.verticalID}-process-${p.processID}-idx-${processIndex}`}>
                                <button
                                    onClick={() => {
                                        setSelectedProcess(p);
                                        setActiveTab("equipment");
                                    }}
                                    className={`w-full rounded-lg px-3 py-3 text-sm text-left transition ${selected
                                        ? "bg-blue-50 border-2 border-blue-300 shadow-sm"
                                        : "border border-gray-200 hover:border-gray-300 hover:bg-gray-50"
                                        }`}
                                >
                                    <div className="flex items-center justify-between mb-1">
                                        <div className="flex items-center gap-2">
                                            <Workflow className="w-4 h-4 text-green-600 flex-shrink-0" />
                                            <span className="font-medium text-gray-800">
                                                {p.processName}
                                            </span>
                                        </div>
                                        {selected && (
                                            <ChevronRight className="w-4 h-4 text-blue-600" />
                                        )}
                                    </div>
                                    <div className="text-xs text-gray-500 ml-6">
                                        {equipmentCount} Equip • {modelCount} Models • {itemsCount} Items
                                    </div>
                                </button>
                            </li>
                            );
                        })}
                    </ul>
                </div>
            </div>
        );
    };

    /* ------------------- TABS ------------------- */
    const renderTabs = () => {
        const tabs: { id: TabType; label: string; icon: React.ComponentType<{ className?: string }>; count?: number }[] = [
            {
                id: "equipment",
                label: "Equipment & Models",
                icon: Wrench,
                count: selectedProcess?.equipments.length,
            },
            {
                id: "scope",
                label: "Scope of Supply",
                icon: ClipboardList,
                count: selectedProcess?.scopeItems?.length,
            }
        ];

        return (
            <div className="border-b border-gray-200 mb-4">
                <div className="flex gap-1">
                    {tabs.map((tab) => {
                        const Icon = tab.icon;
                        const isActive = activeTab === tab.id;
                        return (
                            <button
                                key={tab.id}
                                onClick={() => setActiveTab(tab.id)}
                                className={`flex items-center gap-2 px-4 py-2 text-sm font-medium border-b-2 transition-colors ${isActive
                                    ? "border-blue-600 text-blue-600"
                                    : "border-transparent text-gray-600 hover:text-gray-800 hover:border-gray-300"
                                    }`}
                            >
                                <Icon className="w-4 h-4" />
                                <span>{tab.label}</span>
                                {tab.count !== undefined && (
                                    <span
                                        className={`px-1.5 py-0.5 rounded-full text-xs ${isActive
                                            ? "bg-blue-100 text-blue-700"
                                            : "bg-gray-100 text-gray-600"
                                            }`}
                                    >
                                        {tab.count}
                                    </span>
                                )}
                            </button>
                        );
                    })}
                </div>
            </div>
        );
    };

    /* ------------------- PROCESS INFO CARD ------------------- */
    const renderProcessInfo = () => {
        if (!selectedProcess) return null;

        return (
            <div className="bg-gradient-to-r from-green-50 to-blue-50 border border-green-200 rounded-lg p-4 mb-4">
                <div className="flex items-start justify-between">
                    <div>
                        <div className="flex items-center gap-2 mb-2">
                            <h4 className="font-semibold text-gray-800 text-base">
                                {selectedProcess.processName}
                            </h4>
                            <CodeBadge text={selectedProcess.processCode} color="bg-green-600" />
                            <span
                                className={`px-2 py-0.5 text-xs rounded-full ${selectedProcess.isActive
                                    ? "bg-green-100 text-green-700"
                                    : "bg-red-100 text-red-700"
                                    }`}
                            >
                                {selectedProcess.isActive ? "Active" : "Inactive"}
                            </span>
                        </div>
                        {selectedProcess.description && (
                            <p className="text-sm text-gray-600">{selectedProcess.description}</p>
                        )}
                    </div>
                </div>
            </div>
        );
    };

    /* ------------------- MAIN RENDER ------------------- */
    if (loading)
        return (
            <div className="flex justify-center py-8">
                <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600"></div>
            </div>
        );

    if (error)
        return (
            <div className="bg-red-50 border border-red-200 rounded-lg p-4">
                <p className="text-red-700 font-medium">Error: {error}</p>
            </div>
        );

    if (!vertical || !vertical.processes || vertical.processes.length === 0) {
        return (
            <section className="bg-white border border-gray-200 rounded-xl shadow-sm overflow-hidden">
                <div className="flex flex-col items-center justify-center text-center p-12">
                    <Workflow className="w-16 h-16 text-gray-300 mb-4" />
                    <h3 className="text-lg font-semibold text-gray-700 mb-2">
                        No Vertical Configuration Found
                    </h3>
                    <p className="text-gray-500 max-w-md">
                        Vertical configuration has not been set up for this quote.
                        Please configure the vertical to view processes, equipment, and scope details.
                    </p>
                </div>
            </section>
        );
    }

    return (
        <section className="bg-white border border-gray-200 rounded-xl shadow-sm overflow-hidden">
            {/* HEADER */}
            <header className="bg-gradient-to-r from-blue-50 to-indigo-50 px-6 py-4 border-b border-gray-200">
                <div className="flex flex-wrap items-center justify-between gap-4">
                    <div className="flex items-center gap-2">
                        <Layers className="w-5 h-5 text-blue-600" />
                        <h3 className="text-lg font-semibold text-gray-800">
                            Vertical Configuration
                        </h3>
                        <span className="text-blue-700 font-semibold">
                            • {vertical.verticalName}
                        </span>
                    </div>

                    <div className="flex items-center gap-6 text-sm">                         
                        <div className="flex items-center gap-2">
                            <span className="text-lg text-gray-600">Total Price:</span>
                            <span className="px-3 py-1 bg-green-100 text-lg text-green-700 rounded-full font-semibold">
                                {formatToLakhs(vertical.total_Price)}
                            </span>
                        </div>
                    </div>
                </div>
            </header>

            {/* CONTENT */}
            <div className="flex h-[calc(100vh-250px)] min-h-[500px]">
                {/* LEFT: Processes Sidebar */}
                <div className="w-64 flex-shrink-0 border-r border-gray-200 p-4 bg-gray-50 overflow-y-auto">
                    {renderProcessList()}
                </div>

                {/* RIGHT: Process Details */}
                <div className="flex-1 p-6 overflow-y-auto">
                    {selectedProcess ? (
                        <>
                            {renderProcessInfo()}
                            {renderTabs()}
                            <div className="bg-white">
                                {activeTab === "equipment" && renderEquipmentTab(selectedProcess)}
                                {activeTab === "scope" && renderScopeTab(selectedProcess)}
                            </div>
                        </>
                    ) : (
                        <div className="flex items-center justify-center h-full">
                            <div className="text-center text-gray-500">
                                <Info className="w-12 h-12 mx-auto mb-3 text-gray-400" />
                                <p className="text-lg font-medium">Select a process to begin</p>
                                <p className="text-sm mt-1">
                                    Choose a process from the list to view its configuration
                                </p>
                            </div>
                        </div>
                    )}
                </div>
            </div>
        </section>
    );
};

export default QuotesVerticalImproved;