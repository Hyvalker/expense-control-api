using ExpenseControl.Core.Entities;
using ExpenseControl.Core.Enums;
using ExpenseControl.Core.Interfaces;
using ExpenseControl.Core.Models;

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

    public async Task<ReportModel> GetReportAsync()
    {
        var allPeople = await _personRepository.GetAllAsync();
        var allTransactions = await _transactionRepository.GetAllAsync();

        var peopleReport = allPeople.Select(p =>
        {
            var pTransactions = allTransactions.Where(t => t.PersonId == p.Id).ToList();
            var inc = pTransactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
            var exp = pTransactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
            return new PersonReportModel(p.Name, inc, exp, inc - exp);
        }).ToList();
        
        var totalInc = peopleReport.Sum(r => r.TotalIncome);
        var totalExp = peopleReport.Sum(r => r.TotalExpense);
        
        return new ReportModel(peopleReport, totalInc, totalExp, totalInc - totalExp);
    }
}