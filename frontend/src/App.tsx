import {BrowserRouter, Routes, Route, Link} from 'react-router-dom';
import { Dashboard } from './pages/Dashboard';
import { PeoplePage} from "./pages/PeoplePage.tsx";
import {TransactionsPage} from "./pages/TransactionPage.tsx";

function App() {
    return (
        <div className="min-h-screen bg-gray-50 text-gray-900">
            <BrowserRouter>
                <nav className="p-4 bg-white border-b flex gap-4">
                    <Link to="/">Dashboard</Link>
                    <Link to="/people">Pessoas</Link>
                    <Link to="/transactions">Transações</Link>
                </nav>
                
                <Routes>
                    <Route path="/" element={<Dashboard />} />
                    <Route path="/people" element={<PeoplePage />} />
                    <Route path="/transactions" element={<TransactionsPage />} />
                </Routes>
            </BrowserRouter>
        </div>
    )
}
export default App;



