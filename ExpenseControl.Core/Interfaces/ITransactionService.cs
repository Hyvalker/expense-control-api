using ExpenseControl.Core.Entities;
using ExpenseControl.Core.Models;

namespace ExpenseControl.Core.Interfaces;

public interface ITransactionService
{
    /// <summary>
    /// Cria uma nova transação no banco de dados.
    /// </summary>
    Task<Transaction> CreateAsync(Transaction transaction);
    
    /// <summary>
    ///  Busca uma transação pelo identificador único.
    /// </summary>
    Task<Transaction?> GetByIdAsync(int id);
    
    /// <summary>
    /// Lista todas as transações cadastradas.
    /// </summary>
    Task<IEnumerable<Transaction>> GetAllAsync();
    
    /// <summary>
    /// Busca o relatório total de gastos e despesas.
    /// </summary>
    Task<ReportModel> GetReportAsync();
}