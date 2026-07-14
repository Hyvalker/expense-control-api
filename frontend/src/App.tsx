import {BrowserRouter, Routes, Route, Link} from 'react-router-dom';
import { Dashboard } from './pages/Dashboard';
import { PeoplePage} from "./pages/PeoplePage.tsx";
import {TransactionsPage} from "./pages/TransactionPage.tsx";

/**
 * Componente raiz da aplicação.
 * Configura o sistema de rotas (React Router) e layout base de navegação.
 */
function App() {
    return (
        <div className="min-h-screen bg-gray-50 text-gray-900">
            
            {/* NAVEGAÇÃO GLOBAL: Links principais de acesso às páginas */}
            <BrowserRouter>
                <nav className="p-4 bg-white border-b flex gap-4">
                    <Link to="/">Dashboard</Link>
                    <Link to="/people">Pessoas</Link>
                    <Link to="/transactions">Transações</Link>
                </nav>

                {/* ROTAS: Definição do mapeamento de URLs para os componentes de página */}
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



