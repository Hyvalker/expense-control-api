using ExpenseControl.Core.Entities;

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
}