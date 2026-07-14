using ExpenseControl.Core.Entities;
using ExpenseControl.Core.Interfaces;
using ExpenseControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControl.Infrastructure.Repositories;

/// <summary>
/// Repositório responsável pelas operações de persistência e leitura de dados da entidade Transaction.
/// Implementa a interface ITransactionRepository utilizando o Entity Framework Core.
/// </summary>
public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adiciona uma nova transação ao contexto e persiste as alterações no banco de dados.
    /// </summary>
    public async Task AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Busca uma transação no banco de dados através de seu identificador único.
    /// </summary>
    public async Task<Transaction?> GetByIdAsync(int id)
    {
        return await _context.Transactions.FindAsync(id);
    }

    /// <summary>
    /// Busca todas as transações no banco de dados e às retorna em uma lista, incluindo os dados da pessoa vinculada.
    /// </summary>
    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _context.Transactions
            .Include(t => t.Person)
            .ToListAsync();
    }

    /// <summary>
    /// Busca todas as transações vinculadas à uma pessoa específica.
    /// </summary>
    /// <param name="personId">ID da pessoa proprietária das transações.</param>
    public async Task<IEnumerable<Transaction>> GetByPersonIdAsync(int personId)
    {
        return await _context.Transactions
            .Include(t => t.Person)
            .Where(x => x.PersonId == personId)
            .AsNoTracking() // Performance: não rastreia a entidade, pois é apenas leitura
            .ToListAsync();
    }
    
    /// <summary>
    /// Remove uma transação do banco de dados e confirma a transação.
    /// </summary>
    public async Task DeleteAsync(Transaction transaction)
    {
        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
    }
}