import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Dashboard } from './pages/Dashboard';

function App() {
    return (
        <div className="min-h-screen bg-gray-50 text-gray-900">
            <BrowserRouter>
                <Routes>
                    < Route path="/" element={<Dashboard />} />
                </Routes>
            </BrowserRouter>
        </div>
    )
}
export default App;



