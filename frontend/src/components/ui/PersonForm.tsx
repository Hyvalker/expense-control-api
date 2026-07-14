import { useState } from "react";
import { personService } from "../../api/personService";

interface PersonFormProps {
    onSuccess: () => void;
}

export const PersonForm = ({ onSuccess }: PersonFormProps) => {
    const [name, setName] = useState('');
    const [age, setAge] = useState('');
    const [message, setMessage] = useState<{ text: string, type: 'success' | 'error' } | null>(null);

    const handleSubmit = async (e: React.SyntheticEvent) => {
        e.preventDefault();
        setMessage(null);

        // Validações
        if (name.trim() === '') {
            setMessage({ text: "O nome é obrigatório.", type: 'error' });
            return;
        }
        const ageNum = parseInt(age);
        if (isNaN(ageNum) || ageNum < 0) {
            setMessage({ text: "A idade deve ser um número positivo.", type: 'error' });
            return;
        }

        try {
            await personService.create({ name, age: ageNum });
            setMessage({ text: "Pessoa cadastrada com sucesso!", type: 'success' });
            setName('');
            setAge('');
            onSuccess(); 
        } catch (err: any) {
            const errorData = err.response?.data;
            setMessage({ text: errorData?.Message || "Erro ao salvar pessoa.", type: 'error' });
        }
    };

    return (
        <form onSubmit={handleSubmit} className="mb-8 p-4 bg-white rounded shadow flex gap-2">
            {message && (
                <div className={`p-2 mb-2 w-full rounded text-sm ${message.type === 'success' ? 'bg-green-100 text-green-700' : 'bg-red-100 text-red-700'}`}>
                    {message.text}
                </div>
            )}
            <input placeholder="Nome" value={name} onChange={e => setName(e.target.value)} className="p-2 border rounded flex-1" required />
            <input type="number" placeholder="Idade" value={age} onChange={e => setAge(e.target.value)} className="p-2 border rounded w-24" required />
            <button type="submit" className="px-4 py-2 bg-green-600 text-white rounded">Cadastrar</button>
        </form>
    );
};