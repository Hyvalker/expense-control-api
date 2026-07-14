import { useState } from "react";
import { personService } from "../../api/personService";

/**
 * Propriedades esperadas pelo componente PersonForm.
 */
interface PersonFormProps {
    /** Callback executado após sucesso do cadastro, ideal para atualizar listagens. */
    onSuccess: () => void;
}

/**
 * Formulário para cadastro de novas pessoas.
 * Gerencia o estado dos inputs, validações básicas e integração com a API.
 * 
 * @param onSuccess - Função chamada após a criação bem-sucedida da pessoa.
 */
export const PersonForm = ({ onSuccess }: PersonFormProps) => {
    const [name, setName] = useState('');
    const [age, setAge] = useState('');
    /** Estado para mensagens de feedback (sucesso ou erro) exibidas no formulário. */
    const [message, setMessage] = useState<{ text: string, type: 'success' | 'error' } | null>(null);

    /**
     * Manipula o envio do formulário, realiza as validações e envia os dados para a API.
     */
    const handleSubmit = async (e: React.SyntheticEvent) => {
        e.preventDefault();
        setMessage(null);

        // Validações locais antes de enviar.
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
            // Limpa o formulário após o sucesso.
            setName('');
            setAge('');
            onSuccess(); 
        } catch (err: any) {
            // Tenta extrair a mensagem de erro da API, se disponível.
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