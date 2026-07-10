import { useState, useEffect } from 'react';
import { transactionService } from '../api/transactionService';
import { type Transaction } from '../types/models';

export const useDashboard = () => {
    const [transactions, setTransactions] = useState<Transaction[]>([]);
    const [loading, setLoading] = useState(true);
    const [refreshTrigger, setRefreshTrigger] = useState(0);

   
    
    useEffect(() => {
        setLoading(true);
        transactionService.getAll().then((data) => {
            setTransactions(data);
            setLoading(false);
        });
    }, [refreshTrigger]);
    
    const refresh = () => setRefreshTrigger(prev => prev + 1);

    // Lógica de negócio: Cálculo de totais
    const totals = transactions.reduce((acc, t) => {
        if (t.type === 'Income') acc.income += t.amount;
        else acc.expense += t.amount;
        return acc;
    }, { income: 0, expense: 0 });

    return {
        transactions,
        loading,
        totals,
        balance: totals.income - totals.expense,
        refresh
    };
    
    
};