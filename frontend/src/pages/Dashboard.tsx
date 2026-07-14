import { useState } from 'react';
import { useDashboard } from '../hooks/useDashboard';
import { Card } from '../components/ui/Card';
import { TransactionTable } from '../components/ui/TransactionTable';
import { transactionService } from '../api/transactionService';

export const Dashboard = () => {
    const { loading, totals, balance, people, transactions, refresh } = useDashboard();
    const [view, setView] = useState<'summary' | 'report' | 'person' | 'transactions'>('summary');
    const [selectedPersonId, setSelectedPersonId] = useState<number | null>(null);
    const [searchTerm, setSearchTerm] = useState('');

    const changeView = (newView: typeof view) => {
        setView(newView);
        setSelectedPersonId(null);
        setSearchTerm('');
    };

    if (loading) return <div>Carregando...</div>;

    const handleDelete = async (id: number) => {
        if (confirm("Deseja excluir esta transação?")) {
            try {
                await transactionService.delete(id);
                refresh();
            } catch (error) {
                console.error("Erro ao excluir.", error);
                alert("Não foi possível excluir a transação.");
            }
        }
    };

    return (
        <div className="p-8 max-w-6xl mx-auto">
            <h1 className="text-2xl font-bold mb-6 text-gray-800">Painel de Controle</h1>

            <div className="flex gap-4 mb-8">
                <button onClick={() => changeView('summary')} className={`px-4 py-2 rounded ${view === 'summary' ? 'bg-blue-600 text-white' : 'bg-gray-200'}`}>Visão Geral</button>
                <button onClick={() => changeView('report')} className={`px-4 py-2 rounded ${view === 'report' ? 'bg-blue-600 text-white' : 'bg-gray-200'}`}>Consulta de Totais</button>
                <button onClick={() => changeView('person')} className={`px-4 py-2 rounded ${view === 'person' ? 'bg-blue-600 text-white' : 'bg-gray-200'}`}>Buscar Pessoa</button>
                <button onClick={() => changeView('transactions')} className={`px-4 py-2 rounded ${view === 'transactions' ? 'bg-blue-600 text-white' : 'bg-gray-200'}`}>Listar Transações</button>
            </div>

            {view === 'summary' && (
                <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
                    <Card title="Receitas Totais" value={`R$ ${totals.income.toFixed(2)}`} color="green"/>
                    <Card title="Despesas Totais" value={`R$ ${totals.expense.toFixed(2)}`} color="red"/>
                    <Card title="Saldo Geral" value={`R$ ${balance.toFixed(2)}`} color="blue"/>
                </div>
            )}

            {view === 'report' && (
                <table className="w-full border-collapse bg-white shadow-md">
                    <thead>
                    <tr className="bg-gray-100 border-b">
                        <th className="p-3 text-left">Pessoa</th>
                        <th className="p-3 text-center">Idade</th>
                        <th className="p-3 text-right">Receitas</th>
                        <th className="p-3 text-right">Despesas</th>
                        <th className="p-3 text-right">Saldo</th>
                    </tr>
                    </thead>
                    <tbody>
                    {people.map(p => {
                        const pTrans = transactions.filter(t => t.personId === p.id);
                        const inc = pTrans.filter(t => t.type === 'Income').reduce((s, t) => s + t.amount, 0);
                        const exp = pTrans.filter(t => t.type === 'Expense').reduce((s, t) => s + t.amount, 0);
                        return (
                            <tr key={p.id} className="border-b hover:bg-gray-50">
                                <td className="p-3">{p.name}</td>
                                <td className="p-3 text-center">{p.age}</td>
                                <td className="p-3 text-right text-green-600">R$ {inc.toFixed(2)}</td>
                                <td className="p-3 text-right text-red-600">R$ {exp.toFixed(2)}</td>
                                <td className="p-3 text-right font-bold">R$ {(inc - exp).toFixed(2)}</td>
                            </tr>
                        );
                    })}
                    </tbody>
                </table>
            )}

            {view === 'person' && (
                <div className="bg-white p-6 border rounded shadow">
                    <h2 className="text-xl font-bold mb-4">Buscar Pessoa por Nome</h2>
                    <input
                        type="text"
                        placeholder="Digite o nome..."
                        className="w-full p-2 border mb-6 rounded"
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                    />
                    {searchTerm && (
                        <div className="mb-6 border rounded">
                            {people.filter(p => p.name.toLowerCase().includes(searchTerm.toLowerCase())).map(p => (
                                <button key={p.id} className="block w-full text-left p-2 hover:bg-blue-50 border-b" onClick={() => setSelectedPersonId(p.id)}>
                                    {p.name} 
                                </button>
                            ))}
                        </div>
                    )}
                    {selectedPersonId && (
                        <div>
                            <h3 className="font-bold text-lg mb-4">Transações de: {people.find(p => p.id === selectedPersonId)?.name}</h3>
                            <TransactionTable
                                transactions={transactions.filter(t => t.personId === selectedPersonId)}
                                onDelete={handleDelete}
                            />
                        </div>
                    )}
                </div>
            )}

            {view === 'transactions' && (
                <div className="bg-white p-6 border rounded shadow">
                    <h2 className="text-xl font-bold mb-4">Todas as Transações</h2>
                    <TransactionTable
                        transactions={transactions}
                        onDelete={handleDelete}
                    />
                </div>
            )}
        </div>
    );
};
    