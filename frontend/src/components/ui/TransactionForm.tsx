import {useEffect, useState} from "react";
import {personService} from '../../api/personService';
import {transactionService} from '../../api/transactionService';
import {type Person} from '../../types/models';

/**
 * Propriedades esperadas pelo componente TransactionForm.
 */
interface TransactionFormProps {
    /** Callback executado após o sucesso do salvamento. */
    onSuccess: () => void;
    /** Callback executado ao clicar no botão cancelar. */
    onCancel: () => void;
}

/**
 * Formulário para criação de novas transações financeiras.
 * Carrega dinamicamente a lista de pessoas disponíveis e gerencia o envio do formulário.
 */

export const TransactionForm = ({onSuccess, onCancel}: TransactionFormProps) => {
    const [people, setPeople] = useState<Person[]>([]);
    const [description, setDescription] = useState<string>('');
    const [amount, setAmount] = useState('');
    const [type, setType] = useState('Income');
    const [personId, setPersonId] = useState('');
    /** Mensagem de feedback exibida após a tentativa de cadastro. */
    const [message, setMessage] = useState<{ text: string, type: 'success' | 'error' } | null>(null);

    // Carrega a lista de pessoas ao montar o componente para preencher o select.
    useEffect(() => {
        personService.getAll().then(setPeople);
    }, []);

    /**
     * Manipula o envio do formulário, processa os dados e chama o serviço de transação.
     * @param e
     */
    const handleSubmit = async (e: React.SyntheticEvent) => {
        e.preventDefault();
        setMessage(null);

        try {
            await transactionService.create({
                description,
                amount: parseFloat(amount),
                type: type as any,
                personId: parseInt(personId)
            });

            setMessage({text: "Transação cadastrada com sucesso!", type: 'success'});

            // Aguarda 1.5 segundos para o usuário ler a mensagem de sucesso antes de fechar o formulário.
            setTimeout(() => {
                onSuccess();
            }, 1500);
        } catch (err: any) {

            const errorData = err.response?.data;

            const errorMsg = errorData?.Message || "Erro ao salvar transação.";

            setMessage({text: errorMsg, type: 'error'});
        }
    };
    return (
        <form onSubmit={handleSubmit} className="flex flex-col gap-4 p-6 bg-white rounded-lg shadow-xl w-96">
            <h2 className="text-xl font-bold text-gray-800">Nova Transação</h2>

            {message && (
                <div
                    className={`p-3 rounded text-sm ${message.type === 'success' ? 'bg-green-100 text-green-700' : 'bg-red-100 text-red-700'}`}>
                    {message.text}
                </div>
            )}

            <input
                className="p-2 border rounded"
                placeholder="Descrição"
                onChange={(e) => setDescription(e.target.value)}
                required
            />

            <input
                className="p-2 border rounded"
                type="number"
                step="0.01"
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