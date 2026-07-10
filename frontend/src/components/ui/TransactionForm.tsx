import { useEffect, useState } from "react";
import { personService } from '../../api/personService';
import { transactionService } from '../../api/transactionService';
import { type Person } from '../../types/models';

interface TransactionFormProtps {
    onSuccess: () => void;
    onCancel: () => void;
}

export const TransactionForm = ({ onSuccess, onCancel }: TransactionFormProtps) => {
    const [people, setPeople] = useState<Person[]>([]);
    const [description, setDescription] = useState<string>('');
    const [amount, setAmount] = useState('');
    const [type, setType] = useState('Income');
    const [personId, setPersonId] = useState('');
    
    useEffect(() => {
        personService.getAll().then(setPeople);
    }, []);
    
    const handleSubmit = async (e: React.SyntheticEvent)  => {
        e.preventDefault();
        
        await transactionService.create({
            description,
            amount: parseFloat(amount),
            type: type as any,
            personId: parseInt(personId)
        });
        
        onSuccess();
    };
    return (
        <form onSubmit={handleSubmit} className="flex flex-col gap-4 p-6 bg-white rounded-lg shadow-xl w-96">
            <h2 className="text-xl font-bold text-gray-800">Nova Transação</h2>
            
            <input
            className="p-2 border rounded"
            placeholder="Descrição"
            onChange={(e) => setDescription(e.target.value)}
            required
            />
            
            <input
            className="p-2 border rounded"
            type="number"
            placeholder="Valor"
            onChange={(e) => setAmount(e.target.value)}
            required
            />
            
            <select className="p-2 border rounded" onChange={(e => setPersonId(e.target.value))} required>
                <option value="">Seleciona uma pessoa</option>
                {people.map(p => <option key={p.id} value={p.id}>{p.name}</option>)}
            </select>
            
            <select className="p-2 border rounded" onChange={(e => setType(e.target.value))}>
                <option value="Income">Receita</option>
                <option value="Expense">Despesa</option>
            </select>
            
            <div className="flex gap-2 justify-end">
                <button type="button" onClick={onCancel} className={"px-4 py-2 text-gray-600"}>Cancelar</button>
                <button type="submit" className="px-4 py-2 bg-blue-600 text-white rounded">Salvar</button>
            </div>
        </form>
    )
    
}