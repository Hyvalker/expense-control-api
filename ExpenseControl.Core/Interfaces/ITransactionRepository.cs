using ExpenseControl.Core.Entities;

namespace ExpenseControl.Core.Interfaces;

public interface ITransactionRepository
{
    /// <summary>
    /// Adiciona uma nova transação ao sistema.
    /// </summary>
    Task AddAsync(Transaction transaction);

    /// <summary>
    /// Busca uma transação pelo seu identificador único (Id).
    /// </summary>
    Task<Transaction?> GetByIdAsync(int id);
    
    /// <summary>
    /// Retorna todas as transações cadastradas no sistema.
    /// </summary>
    Task<IEnumerable<Transaction>> GetAllAsync();
    
    /// <summary>
    /// Busca todas as transações associadas a uma pessoa através do identificador único dessa pessoa.
    /// </summary>
    Task <IEnumerable<Transaction>> GetByPersonIdAsync(int personId);
    
}