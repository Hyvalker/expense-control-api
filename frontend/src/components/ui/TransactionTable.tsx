import { type Transaction } from '../../types/models';
import { Button } from './Button';

/**
 * Propriedades esperadas pelo componente TransactionTable.
 */
interface TransactionTableProps {
    /** Lista de transações a serem exibidas na tabela.*/
    transactions: Transaction[];
    /** Callback opcional para exclusão de uma transação pelo seu ID.*/
    onDelete?: (id: number) => void;
}

/**
 * Componente de tabela para exibição de transações financeiras.
 * Formata os valores monetários e diferencia visualmente as receitas de despesas.
 * 
 * @param transactions - Array de objetos de transação.
 * @param onDelete - Função chamada ao clicar no botão de excluir.
 */
export const TransactionTable = ({ transactions, onDelete }: TransactionTableProps) => {
    return (
        <div className="overflow-x-auto">
            <table className="w-full text-left border-collapse">
                <thead>
                <tr className="border-b text-gray-500 text-sm">
                    <th className="py-3 px-4">Descrição</th>
                    <th className="py-3 px-4">Pessoa</th>
                    <th className="py-3 px-4 text-right">Valor</th>
                    <th className="py-3 px-4 text-center">Ações</th>
                </tr>
                </thead>
                <tbody>
                {transactions.map((t) => (
                    <tr key={t.id} className="border-b hover:bg-gray-50">
                        <td className="py-3 px-4 text-gray-800">{t.description}</td>
                        <td className="py-3 px-4 text-gray-600">{t.personName}</td>
                        <td className={`py-3 px-4 text-right font-medium ${t.type === 'Expense' ? 'text-red-600' : 'text-green-600'}`}>
                            {t.type === 'Expense' ? '- ' : '+ '} R$ {Math.abs(t.amount).toFixed(2)}
                        </td>
                        <td className="py-3 px-4 text-center">
                            {onDelete && (
                                <Button variant="danger" onClick={() => onDelete(t.id)}>
                                    Excluir
                                </Button>
                            )}
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    );
};