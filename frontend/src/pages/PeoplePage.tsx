import {useState, useEffect} from "react";
import {personService} from "../api/personService.ts";
import {type Person} from '../types/models';

export const PeoplePage = () => {
    const [people, setPeople] = useState<Person[]>([]);
    const [name, setName] = useState('');
    const [age, setAge] = useState('');

    useEffect(() => {
        loadPeople().catch(console.error);
    }, []);
    const loadPeople = async () => {
        const data = await personService.getAll();
        setPeople(data);
    };

    const handleSubmit = async (e: React.SyntheticEvent) => {
        e.preventDefault();
        await personService.create({name, age: parseInt(age)});
        setName('');
        setAge('');
        await loadPeople();
    };

    const handleDelete = async (id: number) => {
        if (confirm(`Tem certeza? Isso apagrá todas as transações desta pessoa!`)) {
            await personService.delete(id);
            await loadPeople();
        }
    };
    return (
        <div className="p-8 max-w-4xl mx-auto">
            <h1 className="text-2xl font-bold mb-6">Gerenciar Pessoas</h1>

            {/* Formulário de Cadastro */}
            <form onSubmit={handleSubmit} className="mb-8 p-4 bg-white rounded shadow flex gap-2">
                <input
                    placeholder="Nome"
                    value={name}
                    onChange={e => setName(e.target.value)}
                    className="p-2 border rounded flex-1" required
                />
                <input
                    type="number"
                    placeholder="Idade"
                    value={age}
                    onChange={e => setAge(e.target.value)}
                    className="p-2 border rounded w-24" required
                />
                <button type="submit" className="px-4 py-2 bg-green-600 text-white rounded">
                    Cadastrar
                </button>
            </form>

            {/* Table de Listagem */}
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
                                <button
                                    onClick={() => handleDelete(p.id)}
                                    className="text-red-600 hover:underline"
                                >
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