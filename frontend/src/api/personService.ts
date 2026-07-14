import {api} from './axios';
import {type Person} from "../types/models.ts";

/**
 * Serviço responsável pelas operações CRUD da entidade Person.
 * Centraliza as chamadas da API para o endpoint /people.
 */
export const personService = {

    /**
     * Busca todas as pessoas cadastradas.
     * @returns Uma promessa que resolve para um array de objetos Person.
     */
    async getAll(): Promise<Person[]> {
        const response = await api.get<Person[]>('/people');
        return response.data;
    },

    /**
     * Busca uma pessoa específica pelo seu ID.
     * @param id - O identificador único da pessoa.
     * @returns Uma promessa que resolve para o objeto Person encontrado.
     */
    async getById(id: number): Promise<Person> {
        const response = await api.get<Person>(`/people/${id}`);
        return response.data;
    },

    /**
     * Cria uma nova pessoa no banco de dados.
     * @param data - O objeto com os dados da pessoa, omitindo o seu ID.
     * @returns A resposta da requisição POST.
     */
    async create (data: Omit<Person, 'id'>) {
        return await api.post('/people', data);
    },

    /**
     * Remove uma pessoa do banco de dados através de seu ID.
     * @param id - O identificador único da pessoa a ser removida.
     * @returns A resposta da requisição DELETE.
     */
    async delete (id: number) {
        return await api.delete(`/people/${id}`);
    }
}