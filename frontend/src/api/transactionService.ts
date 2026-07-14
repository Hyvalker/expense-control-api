import { api } from './axios';
import { type Transaction, TRANSACTION_TYPE_MAP, BACKEND_TYPE_MAP} from "../types/models.ts";


export const transactionService = {
    
    async getAll(): Promise<Transaction[]> {
        const response = await api.get<Transaction[]>('/transactions');
        
        return response.data.map(t => ({
            ...t,
            type: TRANSACTION_TYPE_MAP[(t.type as unknown) as number],
            personName: t.personName,
        }));
    },
    
    async create (data: Omit<Transaction, 'id' | 'personName'>) {
        
        const payload = {
            ...data,
            type: BACKEND_TYPE_MAP[data.type]
        }
        
        return await api.post('/transactions', payload);
    },

    async delete(id: number) {
        return await api.delete(`/transactions/${id}`);
    }
}
