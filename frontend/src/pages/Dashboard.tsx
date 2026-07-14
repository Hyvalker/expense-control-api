import {useState} from 'react';
import {useDashboard} from '../hooks/useDashboard';
import {Card} from '../components/ui/Card';
import {TransactionTable} from '../components/ui/TransactionTable';
import {transactionService} from '../api/transactionService';
import { ReportPage } from './ReportPage';

/**
 * Componente principal da aplicação.
 * Gerencia as visualizações do Dashboard, incluindo resumos financeiros, relatórios por pessoas
 * e listagem de transações.
 */
export const Dashboard = () => {
    // Hooks e estados globais de dados.
    const {loading, totals, balance, people, transactions, refresh} = useDashboard();

    /** Define a visualização ativa no momento. */
    const [view, setView] = useState<'summary' | 'report' | 'person' | 'transactions'>('summary');

    /** ID da pessoa selecionada para filtro de transações. */
    const [selectedPersonId, setSelectedPersonId] = useState<number | null>(null);

    /** Termo de busca para filtrar pessoas. */
    const [searchTerm, setSearchTerm] = useState('');

    /**
     * Alterna entre as abas do dashboard e reseta filtros aplicados.
     */
    const changeView = (newView: typeof view) => {
        setView(newView);
        setSelectedPersonId(null);
        setSearchTerm('');
    };

    if (loading) return <div>Carregando...</div>;

    /**
     * Gerencia a exclusão de uma transação com confirmação via browser.
     * Atualiza o estado global após a deleção.
     */
    const handleDelete = async (id: number) => {
        if (confirm("Deseja excluir esta transação?")) {
            try {
                await transactionService.delete(id);
                refresh();
            } catch (error) {
                console.error("Erro ao excluir.", error);
                alert("Não foi possível excluir a transação.");
            }
        }
    };

    return (
        <div className="p-8 max-w-6xl mx-auto">
            <h1 className="text-2xl font-bold mb-6 text-gray-800">Painel de Controle</h1>

            {/* NAV: Alternador de estados de visualização do dashboard. */}
            <div className="flex gap-4 mb-8">
                <button onClick={() => changeView('summary')}
                        className={`px-4 py-2 rounded ${view === 'summary' ? 'bg-blue-600 text-white' : 'bg-gray-200'}`}>Visão
                    Geral
                </button>
                <button onClick={() => changeView('report')}
                        className={`px-4 py-2 rounded ${view === 'report' ? 'bg-blue-600 text-white' : 'bg-gray-200'}`}>Consulta
                    de Totais
                </button>
                <button onClick={() => changeView('person')}
                        className={`px-4 py-2 rounded ${view === 'person' ? 'bg-blue-600 text-white' : 'bg-gray-200'}`}>Buscar
                    Pessoa
                </button>
                <button onClick={() => changeView('transactions')}
                        className={`px-4 py-2 rounded ${view === 'transactions' ? 'bg-blue-600 text-white' : 'bg-gray-200'}`}>Listar
                    Transações
                </button>
            </div>

            {/* SEÇÃO SUMARY: Exibe os cards de indicadores financeiros totais. */}
            {view === 'summary' && (
                <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
                    <Card title="Receitas Totais" value={`R$ ${totals.income.toFixed(2)}`} color="green"/>
                    <Card title="Despesas Totais" value={`R$ ${totals.expense.toFixed(2)}`} color="red"/>
                    <Card title="Saldo Geral" value={`R$ ${balance.toFixed(2)}`} color="blue"/>
                </div>
            )}
            
            {/* SEÇÃO REPORT: Tabela da análise financeira por pessoa. */}
            {view === 'report' && (
                <ReportPage people={people} transactions={transactions} />
            )}
            
            {/* SEÇÃO PERSON: Busca de transações filtradas por uma pessoa específica. */}
            {view === 'person' && (
                <div className="bg-white p-6 border rounded shadow">
                    <h2 className="text-xl font-bold mb-4">Buscar Pessoa por Nome</h2>
                    <input
                        type="text"
                        placeholder="Digite o nome..."
                        className="w-full p-2 border mb-6 rounded"
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                    />
                    {searchTerm && (
                        <div className="mb-6 border rounded">
                            {people.filter(p => p.name.toLowerCase().includes(searchTerm.toLowerCase())).map(p => (
                                <button key={p.id} className="block w-full text-left p-2 hover:bg-blue-50 border-b"
                                        onClick={() => setSelectedPersonId(p.id)}>
                                    {p.name}
                                </button>
                            ))}
                        </div>
                    )}
                    {selectedPersonId && (
                        <div>
                            <h3 className="font-bold text-lg mb-4">Transações
                                de: {people.find(p => p.id === selectedPersonId)?.name}</h3>
                            <TransactionTable
                                transactions={transactions.filter(t => t.personId === selectedPersonId)}
                                onDelete={handleDelete}
                            />
                        </div>
                    )}
                </div>
            )}
            
            {/* SEÇÃO TRANSACTIONS: Lista completa com funcionalidade de deleção.*/}
            {view === 'transactions' && (
                <div className="bg-white p-6 border rounded shadow">
                    <h2 className="text-xl font-bold mb-4">Todas as Transações</h2>
                    <TransactionTable
                        transactions={transactions}
                        onDelete={handleDelete}
                    />
                </div>
            )}
        </div>
    );
};
    