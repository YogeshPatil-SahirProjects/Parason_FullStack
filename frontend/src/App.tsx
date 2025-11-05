import { useState } from 'react';
import Navbar from './Component/Navbar';
import QuotesTable from './Component/QuotesTable';
import About from './Component/About';
import QuoteForm from './Component/QuoteForm';

interface User {
    name: string;
    email: string;
}

type Page = 'home' | 'about' | 'createQuote';

function App() {
    const [currentUser] = useState<User>({
        name: 'Yogesh Patil',
        email: 'YogeshP@SahirProjects.com'
    });

    const [currentPage, setCurrentPage] = useState<Page>('home');

    const handleLogout = () => {
        console.log('Logging out...');
        // Add your logout logic here
        // Example: navigate to login page, clear tokens, etc.
    };

    const handleNavigate = (page: Page) => {
        setCurrentPage(page);
    };

    return (
        <div className="min-h-screen bg-gray-50">
            <Navbar
                currentUser={currentUser}
                onLogout={handleLogout}
                onNavigate={handleNavigate}
                currentPage={currentPage}
            />
            {currentPage === 'home' ? (
                <main className="container mx-auto px-2 py-2">
                    <QuotesTable onNavigate={handleNavigate} />
                </main>
            ) : currentPage === 'createQuote' ? (
                <QuoteForm onNavigate={handleNavigate} />
            ) : (
                <About />
            )}
        </div>
    );
}

export default App;