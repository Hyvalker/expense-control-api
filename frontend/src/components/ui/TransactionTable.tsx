import { type Transaction } from '../../types/models';

interface TransactionTableProps {
    transactions: Transaction[];
}

export const TransactionTable = ({transactions}: TransactionTableProps) => {
    return (
        <div className="overflow-x-auto">
            <table className="w-full text-left border-collapse">
                <thead>
                <tr className="border-b text-gray-500 text-sm">
                    <th className="py-3 px-4">Descrição</th>
                    <th className="py-3 px-4">Pessoa</th>
                    <th className="py-3 px-4 text-right">Valor</th>
                </tr>
                </thead>
                <tbody>
                {transactions.map((t) => (
                    <tr key={t.id} className="bordar-b hover:bg-gray-50">
                    <td className="py-3 px-4 text-gray-800">{t.description}</td>
                    <td className="py-3 px-4 text-gray-600">{t.personName}</td>
                <td className={`py-3 px-4 text-right font-medium ${t.type === 'Expense' ? 'text-red-600' : 'text-green-600'}`}>
                    {t.type === 'Expense' ? '- ' : '+ '}
                    R$ {Math.abs(t.amount).toFixed(2)}
                </td>
                </tr>
                ))}
            </tbody>
        </table>
</div>
    );
};