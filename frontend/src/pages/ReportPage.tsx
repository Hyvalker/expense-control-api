import { type Person, type Transaction } from '../types/models';

/**
 * Propriedades esperadas pelo componente ReportPage.
 */
interface ReportPageProps {
    /** Lista de pessoas para exibir no relatório. */
    people: Person[];
    /** Lista de todas as transações para cálculo dos saldos. */
    transactions: Transaction[];
}

/**
 * Exibe um relatório financeiro completo pra cada pessoa. 
 * Realiza o filtro e cálculo de receitas, despesas e saldo líquido de cada indivíduo.
 */
export const ReportPage = ({ people, transactions }: ReportPageProps) => {
    return (
        <div className="bg-white p-6 shadow-md rounded-lg">
            <h2 className="text-xl font-bold mb-4">Relatório Consolidado</h2>
            <table className="w-full border-collapse">
                <thead>
                <tr className="bg-gray-100 border-b">
                    <th className="p-3 text-left">Pessoa</th>
                    <th className="p-3 text-center">Idade</th>
                    <th className="p-3 text-right">Receitas</th>
                    <th className="p-3 text-right">Despesas</th>
                    <th className="p-3 text-right">Saldo</th>
                </tr>
                </thead>
                <tbody>
                {people.map(p => {
                    const pTrans = transactions.filter(t => t.personId === p.id);
                    const inc = pTrans.filter(t => t.type === 'Income').reduce((s, t) => s + t.amount, 0);
                    const exp = pTrans.filter(t => t.type === 'Expense').reduce((s, t) => s + t.amount, 0);
                    return (
                        <tr key={p.id} className="border-b hover:bg-gray-50">
                            <td className="p-3">{p.name}</td>
                            <td className="p-3 text-center">{p.age}</td>
                            <td className="p-3 text-right text-green-600">R$ {inc.toFixed(2)}</td>
                            <td className="p-3 text-right text-red-600">R$ {exp.toFixed(2)}</td>
                            <td className="p-3 text-right font-bold">R$ {(inc - exp).toFixed(2)}</td>
                        </tr>
                    );
                })}
                </tbody>
            </table>
        </div>
    );
};