import { api } from './axios';
import { type Transaction, TRANSACTION_TYPE_MAP, BACKEND_TYPE_MAP} from "../types/models.ts";

/**
 * Serviço de operações para transações financeiras.
 * Realiza a ponte entre a API e a interface, aplicando as transformações de mapeamento de tipo necessárias.
 */
export const transactionService = {

    /**
     * Busca todas as transações, convertendo o formato numérico do backend para o formato enum do frontend.
     * @returns Uma promessa que resolve para um array de objetos Transaction com tipos tipados.
     */
    async getAll(): Promise<Transaction[]> {
        const response = await api.get<Transaction[]>('/transactions');
        
        return response.data.map(t => ({
            ...t,
            // Converte o tipo numérico recebido da API para o valor legível definido no mapa.
            type: TRANSACTION_TYPE_MAP[(t.type as unknown) as number],
            personName: t.personName,
        }));
    },

    /**
     * Envia uma nova transação para o backend, convertendo o tipo para o formato esperado pela API.
     * @param data - Objeto da transação contendo os detalhes, sem o ID e o nome da pessoa.
     * @returns A resposta da requisição POST.
     */
    async create (data: Omit<Transaction, 'id' | 'personName'>) {
        // Prepara o payload convertendo o enum do frontend de volta para o número que o backend entende.
        const payload = {
            ...data,
            type: BACKEND_TYPE_MAP[data.type]
        }
        
        return await api.post('/transactions', payload);
    },

    /**
     * Remove uma transação do sistema pelo ID.
     * @param id - Identificador da transação a ser removida.
     * @returns A resposta da requisição DELETE.
     */
    async delete(id: number) {
        return await api.delete(`/transactions/${id}`);
    }
}
