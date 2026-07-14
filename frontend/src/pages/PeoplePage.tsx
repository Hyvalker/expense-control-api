import { useState, useEffect } from "react";
import { personService } from "../api/personService";
import { type Person } from '../types/models';
import { PersonForm } from "../components/ui/PersonForm"; 

export const PeoplePage = () => {
    const [people, setPeople] = useState<Person[]>([]);

    useEffect(() => {
        loadPeople();
    }, []);

    const loadPeople = async () => {
        const data = await personService.getAll();
        setPeople(data);
    };

    const handleDelete = async (id: number) => {
        if (confirm(`Tem certeza? Isso apagará todas as transações desta pessoa!`)) {
            await personService.delete(id);
            await loadPeople();
        }
    };

    return (
        <div className="p-8 max-w-4xl mx-auto">
            <h1 className="text-2xl font-bold mb-6">Gerenciar Pessoas</h1>
            
            <PersonForm onSuccess={loadPeople} />

            <div className="bg-white rounded shadow p-4">
                <table className="w-full">
                    <thead>
                    <tr className="text-left border-b">
                        <th className="p-2">Nome</th>
                        <th className="p-2">Idade</th>
                        <th className="p-2">Ações</th>
                    </tr>
                    </thead>
                    <tbody>
                    {people.map(p => (
                        <tr key={p.id} className="border-b">
                            <td className="p-2">{p.name}</td>
                            <td className="p-2">{p.age}</td>
                            <td className="p-2">
                                <button onClick={() => handleDelete(p.id)} className="text-red-600 hover:underline">
                                    Deletar
                                </button>
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};