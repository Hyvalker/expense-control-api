import { useState } from 'react';
import { useDashboard } from '../hooks/useDashboard';
import { TransactionForm } from '../components/ui/TransactionForm';
import { TransactionTable } from '../components/ui/TransactionTable';
import { transactionService } from '../api/transactionService';

export const TransactionsPage = () => {
    const { transactions, refresh } = useDashboard(); 
    const [isAdding, setIsAdding] = useState(false);

    const handleDelete = async (id: number) => {
        console.log("handleDelete foi chamado com o ID:", id); // Adicione isso aqui

        if (confirm("Tem certeza que deseja excluir esta transação?")) {
            console.log("Confirmou a exclusão, chamando API...");
            await transactionService.delete(id);
            refresh();
        }
    };

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
                <TransactionTable transactions={transactions} onDelete={handleDelete} />
            )}
        </div>
    );
};