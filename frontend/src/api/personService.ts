import {api} from './axios';
import {type Person} from "../types/models.ts";

export const personService = {

    async getAll(): Promise<Person[]> {
        const response = await api.get<Person[]>('/people');
        return response.data;
    },

    async getById(id: number): Promise<Person> {
        const response = await api.get<Person>(`/people/${id}`);
        return response.data;
    },
    
    async create (data: Omit<Person, 'id'>) {
        return await api.post('/people', data);
    },
    
    async delete (id: number) {
        return await api.delete(`/people/${id}`);
    }
}