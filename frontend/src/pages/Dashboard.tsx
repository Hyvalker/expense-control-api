import {useState} from 'react';
import {useDashboard} from '../hooks/useDashboard.ts';
import {Card} from '../components/ui/Card.tsx';
import {TransactionTable} from '../components/ui/TransactionTable';
import {TransactionForm} from '../components/ui/TransactionForm'

export const Dashboard = () => {
    const {loading, totals, balance, transactions, refresh} = useDashboard();

    const [isAdding, setIsAdding] = useState(false);
    if (loading) {
        return (
            <div className="min-h-screen flex items-center justify-center text-xl front-semibold text-gray-500">
                Carregando seus dados...
            </div>
        );
    }

    return (
        <div className="p-8 max-w-6xl mx-auto">
            <h1 className="text-2xl font-bold mb-6 text-gray-800">Painel Financeiro</h1>

            {/* Container dos Cards */}
            <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mb-8">
                <Card title="Receitas Totais" value={`R$ ${totals.income.toFixed(2)}`} color="green"/>
                <Card title="Despesas Totais" value={`R$ ${totals.expense.toFixed(2)}`} color={"red"}/>
                <Card title="Saldo Geral" value={`R$ ${balance.toFixed(2)}`} color="blue"/>
            </div>

            <div className="mb-4">
                <button
                    onClick={() => setIsAdding(!isAdding)}
                    className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700"
                >
                    {isAdding ? 'Ver Transações' : 'Nova Transação'}
                </button>
            </div>
            <div className="bg-white rounded-lg shadow-sm border border-gray-100">
                {isAdding ? (
                    <TransactionForm
                        onSuccess={() => {
                            setIsAdding(false);
                            refresh();
                        }}
                        onCancel={() => setIsAdding(false)}
                    />
                ) : (
                    <div className="bg-white p-6 rounded-lg shadow-sm border border-gray-100">
                        <h2 className="text-lg font-semibold mb-4">Últimas Transações</h2>
                        <TransactionTable transactions={transactions}/>
                    </div>
                )}
            </div>
        </div>
    );
};