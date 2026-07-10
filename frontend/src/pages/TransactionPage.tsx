import { useState } from 'react';
import { useDashboard } from '../hooks/useDashboard';
import { TransactionForm } from '../components/ui/TransactionForm';

export const TransactionsPage = () => {
    const { refresh } = useDashboard(); // Removemos o 'transactions' daqui, pois não vamos mais usar na tabela
    const [isAdding, setIsAdding] = useState(false);

    return (
        <div className="p-8 max-w-4xl mx-auto">
            <div className="flex justify-between items-center mb-6">
                <h1 className="text-2xl font-bold">Transações</h1>
                <button
                    onClick={() => setIsAdding(!isAdding)}
                    className="px-4 py-2 bg-blue-600 text-white rounded"
                >
                    {isAdding ? 'Cancelar' : 'Nova Transação'}
                </button>
            </div>
            
            {isAdding ? (
                <TransactionForm
                    onSuccess={() => { setIsAdding(false); refresh(); }}
                    onCancel={() => setIsAdding(false)}
                />
            ) : (
                <div className="text-gray-500 text-center py-10">
                    Clique em "Nova Transação" para adicionar um novo registro.
                </div>
            )}
        </div>
    );
};