import React, { useState, useRef, useEffect } from 'react';
import { ChevronDown, User, LogOut } from 'lucide-react';

interface MenuItem {
    id: string;
    label: string;
    items: {
        label: string;
        action: () => void;
    }[];
}

interface NavbarProps {
    currentUser: {
        name: string;
        email: string;
    };
    onLogout: () => void;
    onNavigate: (page: 'home' | 'about') => void;
    currentPage: 'home' | 'about';
}

const Navbar: React.FC<NavbarProps> = ({ currentUser, onLogout, onNavigate, currentPage }) => {
    const [activeDropdown, setActiveDropdown] = useState<string | null>(null);
    const dropdownRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        const handleClickOutside = (event: MouseEvent) => {
            if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
                setActiveDropdown(null);
            }
        };

        document.addEventListener('mousedown', handleClickOutside);
        return () => document.removeEventListener('mousedown', handleClickOutside);
    }, []);

    const toggleDropdown = (dropdown: string) => {
        setActiveDropdown(activeDropdown === dropdown ? null : dropdown);
    };

    const menuItems: MenuItem[] = [
        {
            id: 'preferences',
            label: 'Preferences',
            items: [
                { label: 'General Settings', action: () => console.log('General Settings') },
                { label: 'Display Options', action: () => console.log('Display Options') },
                { label: 'Notifications', action: () => console.log('Notifications') }
            ]
        },
        {
            id: 'user-management',
            label: 'User Management',
            items: [
                { label: 'Manage Users', action: () => console.log('Manage Users') },
                { label: 'Roles & Permissions', action: () => console.log('Roles & Permissions') },
                { label: 'User Groups', action: () => console.log('User Groups') }
            ]
        },
        {
            id: 'utility',
            label: 'Utility',
            items: [
                { label: 'Import Data', action: () => console.log('Import Data') },
                { label: 'Export Data', action: () => console.log('Export Data') },
                { label: 'System Tools', action: () => console.log('System Tools') }
            ]
        }
    ];

    return (
        <nav className="bg-gradient-to-r from-blue-500 via-blue-600 to-indigo-600 border-b border-blue-700 shadow-lg">
            <div className="container mx-auto px-4">
                <div className="flex items-center justify-between h-16">
                    {/* Logo/Brand */}
                    <div className="flex items-center space-x-8">
                        <div className="flex items-center space-x-2">
                            <div className="w-8 h-8 bg-white rounded flex items-center justify-center">
                                <span className="text-blue-600 font-bold text-sm">PC</span>
                            </div>
                            <div>
                                <div className="text-sm font-semibold text-white">Parason CPQ</div>
                                <div className="text-xs text-blue-100">Master System</div>
                            </div>
                        </div>

                        {/* Navigation Links */}
                        <div className="flex items-center space-x-1">
                            <button
                                onClick={() => onNavigate('home')}
                                className={`px-4 py-2 text-sm font-medium rounded-md transition-colors ${
                                    currentPage === 'home'
                                        ? 'text-white bg-white/20'
                                        : 'text-blue-100 hover:text-white hover:bg-white/10'
                                }`}
                            >
                                Home
                            </button>
                            <button
                                onClick={() => onNavigate('about')}
                                className={`px-4 py-2 text-sm font-medium rounded-md transition-colors ${
                                    currentPage === 'about'
                                        ? 'text-white bg-white/20'
                                        : 'text-blue-100 hover:text-white hover:bg-white/10'
                                }`}
                            >
                                About
                            </button>
                        </div>
                    </div>

                    {/* Navigation Menu */}
                    <div className="flex items-center space-x-1" ref={dropdownRef}>
                        {menuItems.map((menu) => (
                            <div key={menu.id} className="relative">
                                <button
                                    onClick={() => toggleDropdown(menu.id)}
                                    className="flex items-center space-x-1 px-4 py-2 text-sm font-medium text-white hover:text-blue-100 hover:bg-white/10 rounded-md transition-colors"
                                >
                                    <span>{menu.label}</span>
                                    <ChevronDown className="w-4 h-4" />
                                </button>

                                {activeDropdown === menu.id && (
                                    <div className="absolute top-full left-0 mt-1 w-56 bg-white border border-gray-200 rounded-md shadow-lg z-50">
                                        <div className="py-1">
                                            {menu.items.map((item, idx) => (
                                                <button
                                                    key={idx}
                                                    onClick={() => {
                                                        item.action();
                                                        setActiveDropdown(null);
                                                    }}
                                                    className="w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-gray-50 hover:text-blue-600 transition-colors"
                                                >
                                                    {item.label}
                                                </button>
                                            ))}
                                        </div>
                                    </div>
                                )}
                            </div>
                        ))}

                        {/* User Menu */}
                        <div className="relative ml-4">
                            <button
                                onClick={() => toggleDropdown('user')}
                                className="flex items-center space-x-2 px-4 py-2 text-sm font-medium text-white hover:text-blue-100 hover:bg-white/10 rounded-md transition-colors border-l border-blue-400 ml-2"
                            >
                                <User className="w-4 h-4" />
                                <span>{currentUser.name}</span>
                                <ChevronDown className="w-4 h-4" />
                            </button>

                            {activeDropdown === 'user' && (
                                <div className="absolute top-full right-0 mt-1 w-56 bg-white border border-gray-200 rounded-md shadow-lg z-50">
                                    <div className="px-4 py-3 border-b border-gray-200">
                                        <p className="text-sm font-medium text-gray-900">{currentUser.name}</p>
                                        <p className="text-xs text-gray-500 mt-1">{currentUser.email}</p>
                                    </div>
                                    <div className="py-1">
                                        <button
                                            onClick={() => {
                                                onLogout();
                                                setActiveDropdown(null);
                                            }}
                                            className="w-full flex items-center space-x-2 px-4 py-2 text-sm text-red-600 hover:bg-red-50 transition-colors"
                                        >
                                            <LogOut className="w-4 h-4" />
                                            <span>Logout</span>
                                        </button>
                                    </div>
                                </div>
                            )}
                        </div>
                    </div>
                </div>
            </div>
        </nav>
    );
};

export default Navbar;