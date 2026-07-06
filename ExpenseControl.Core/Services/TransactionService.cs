using ExpenseControl.Core.Entities;
using ExpenseControl.Core.Enums;
using ExpenseControl.Core.Interfaces;

namespace ExpenseControl.Core.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IPersonRepository _personRepository;

    public TransactionService(ITransactionRepository transactionRepository, IPersonRepository personRepository)
    {
        _transactionRepository = transactionRepository;
        _personRepository = personRepository;
    }

    public async Task<Transaction> CreateAsync(Transaction transaction)
    {
        var person = await _personRepository.GetByIdAsync(transaction.PersonId);
        
        // Verifica se a pessoa existe no banco de dados
        if (person == null)
            throw new Exception("Pessoa não encontrada.");

        // Define que menores de 18 anos só possam cadastrar despesas.
        if (person.Age < 18 && transaction.Type == TransactionType.Income)
            throw new InvalidOperationException("Menores de 18 anos não podem registrar receita.");
        
        await _transactionRepository.AddAsync(transaction);
        return transaction;
    }

    public async Task<Transaction?> GetByIdAsync(int id)
    {
        return await _transactionRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _transactionRepository.GetAllAsync();
    }
}