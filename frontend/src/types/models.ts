export interface Person {
    id: number;
    name: string;
    age: number;
}

export type TransactionType = 'Income' | 'Expense';

export interface Transaction {
    id: number;
    description: string;
    amount: number;
    type: TransactionType;
    personId: number;
    personName?: string;
}
export const TRANSACTION_TYPE_MAP: Record<number, TransactionType> = {
    0: 'Income',
    1: 'Expense',
}

export const BACKEND_TYPE_MAP: Record<TransactionType, number> = {
    'Income': 0,
    'Expense': 1
}