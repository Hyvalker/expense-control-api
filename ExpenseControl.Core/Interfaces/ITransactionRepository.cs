using ExpenseControl.Core.Entities;

namespace ExpenseControl.Core.Interfaces;

/// <summary>
/// Define os contratos para operações de persistência da entidade Transaction.
/// </summary>
public interface ITransactionRepository
{
    /// <summary>
    /// Adiciona uma nova transação ao sistema.
    /// </summary>
    /// <param name="transaction">Entidade da transação a ser salva.</param>
    Task AddAsync(Transaction transaction);

    /// <summary>
    /// Busca uma transação pelo seu identificador único (Id).
    /// </summary>
    /// <param name="id">ID da transação.</param>
    /// <returns>A transação encontrada ou nulo.</returns>
    Task<Transaction?> GetByIdAsync(int id);
    
    /// <summary>
    /// Retorna todas as transações cadastradas no sistema.
    /// </summary>
    Task<IEnumerable<Transaction>> GetAllAsync();
    
    /// <summary>
    /// Busca todas as transações associadas a uma pessoa específicaa.
    /// </summary>
    /// <param name="personId">ID da pessoa proprietária das transações.</param>
    /// <returns>Uma coleção de transações pertencentes à pessoa.</returns>
    Task <IEnumerable<Transaction>> GetByPersonIdAsync(int personId);
    
    
    /// <summary>
    /// Deleta uma transação cadastrada no sistema.
    /// </summary>
    /// <param name="transaction">Entidade da transação a ser removida.</param>
    Task DeleteAsync(Transaction transaction);
    
}