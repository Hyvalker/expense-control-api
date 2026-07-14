using ExpenseControl.Core.Entities;
using ExpenseControl.Core.Models;

namespace ExpenseControl.Core.Interfaces;

/// <summary>
/// Contrato de serviço para gerenciamento das regras de negócio de transações financeiras.
/// </summary>
public interface ITransactionService
{
    /// <summary>
    /// Cria uma nova transação no banco de dados após validar as regras de negócio.
    /// </summary>
    /// <param name="transaction">Dados da transação a ser criada.</param>
    /// <returns>A transação criada.</returns>
    /// <exception cref="InvalidOperationException">Lançada caso a pessoa seja menor de 18 anos e a transação seja do tipo Receita.</exception>
    Task<Transaction> CreateAsync(Transaction transaction);
    
    /// <summary>
    ///  Busca uma transação pelo seu identificador único.
    /// </summary>
    /// <param name="id">ID da transação.</param>
    /// <returns>A transação encontrada.</returns>
    /// <exception cref="KeyNotFoundException">Lançada se a transação não existir.</exception>
    Task<Transaction> GetByIdAsync(int id);
    
    /// <summary>
    /// Lista todas as transações cadastradas.
    /// </summary>
    Task<IEnumerable<Transaction>> GetAllAsync();
    
    /// <summary>
    /// Lista o relatório total de receitas e despesas.
    /// </summary>
    /// <returns>Um objeto <see cref="ReportModel"/> com os dados financeiros.</returns>
    Task<ReportModel> GetReportAsync();

    /// <summary>
    /// Lista todas as transações de uma pessoa específica.
    /// </summary>
    /// <param name="personId">ID da pessoa proprietária das transações</param>
    Task<IEnumerable<Transaction>> GetByPersonIdAsync(int personId);
    
    
    /// <summary>
    /// Remove uma transação cadastrada no sistema.
    /// </summary>
    /// <param name="id">ID da transação a ser removida.</param>
    /// <exception cref="KeyNotFoundException">Lançada se a transação não for encontrada.</exception>
    Task DeleteAsync(int id);

}