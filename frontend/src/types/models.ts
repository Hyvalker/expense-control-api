/**
 * Representa uma pessoa cadastrada no sistema.
 */
export interface Person {
    /** Identificador único da pessoa. */
    id: number;
    /** Nome completo da pessoa. */
    name: string;
    /** Idade da pessoa em anos.*/
    age: number;
}

/**
 * Define os tipos possíveis de transações financeiras.
 */
export type TransactionType = 'Income' | 'Expense';

/**
 * Representa uma transação financeira associada à uma pessoa.
 */
export interface Transaction {
    /** Identificador único da transação. */
    id: number;
    /** Breve descrição sobre a movimentação. */
    description: string;
    /** Valor monetário da transação. */
    amount: number;
    /** Tipo da transação: 'Income' (Receita) ou 'Expense' (Despesa) */
    type: TransactionType;
    /** ID da pessoa que realizou a transação. */
    personId: number;
    /** Nome da pessoa associada (opcional, usado para exibições em tabelas). */
    personName?: string;
}

/**
 * Mapeia os tipos de transação de volta para os valores numéricos esperados pelo backend.
 * Essencial para a requisição de criação (POST) ou futura requisição de atualização (PUT), se implementada.
 */
export const TRANSACTION_TYPE_MAP: Record<number, TransactionType> = {
    0: 'Income',
    1: 'Expense',
}

export const BACKEND_TYPE_MAP: Record<TransactionType, number> = {
    'Income': 0,
    'Expense': 1
}