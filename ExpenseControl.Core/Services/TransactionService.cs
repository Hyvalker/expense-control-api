using ExpenseControl.Core.Entities;
using ExpenseControl.Core.Enums;
using ExpenseControl.Core.Interfaces;
using ExpenseControl.Core.Models;

namespace ExpenseControl.Core.Services;

/// <summary>
/// Serviço responsável pelas regras de negócio e gerenciamento de transações.
/// </summary>
public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IPersonRepository _personRepository;

    public TransactionService(ITransactionRepository transactionRepository, IPersonRepository personRepository)
    {
        _transactionRepository = transactionRepository;
        _personRepository = personRepository;
    }

    /// <summary>
    /// Cria uma nova transação após validar as regras de negócio e existência da pessoa vinculada.
    /// </summary>
    /// <param name="transaction">A transação a ser persistida.</param>
    /// <returns>A transação criada com sucesso.</returns>
    /// <exception cref="KeyNotFoundException">Lançada caso o PersonId não exista.</exception>
    /// <exception cref="InvalidOperationException">Lançada caso a transação seja inválida (ex: receita para menores de 18, valor <= 0 ou descrição vazia)</exception>
    public async Task<Transaction> CreateAsync(Transaction transaction)
    {
        var person = await _personRepository.GetByIdAsync(transaction.PersonId);
        
        if (person == null)
            throw new KeyNotFoundException($"Pessoa com ID {transaction.PersonId} não encontrada.");

        // Regra de negócio: Define que menores de 18 anos não podem cadastrar receita.
        if (person.Age < 18 && transaction.Type == TransactionType.Income)
            throw new InvalidOperationException("Menores de 18 anos não podem registrar receita.");

        if (transaction.Amount <= 0)
            throw new InvalidOperationException("O valor da transação deve ser maior que zero.");
        
        if (string.IsNullOrWhiteSpace(transaction.Description))
            throw new InvalidOperationException("A descrição da transação é obrigatória.");
        
        await _transactionRepository.AddAsync(transaction);
        return transaction;
    }

    /// <summary>
    /// Busca uma transação específica pelo ID.
    /// </summary>
    /// <param name="id">Identificador único da transação.</param>
    /// <returns>A transação encontrada.</returns>
    /// <exception cref="KeyNotFoundException">Lançada se a transação não existir.</exception>
    public async Task<Transaction> GetByIdAsync(int id)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id);
        
        if (transaction == null)
            throw new KeyNotFoundException($"Transação com ID {id} não encontrada.");
        
        return transaction;
    }
    
    /// <summary>
    /// Retorna todas as transações cadastradas no sistema.
    /// </summary>
    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _transactionRepository.GetAllAsync();
    }

    /// <summary>
    /// Busca os dados financeiros (receitas e despesas) de todas as pessoas e calcula os totais.
    /// </summary>
    /// <returns>Objeto contendo o relatório detalhado por pessoa e os totais gerais.</returns>
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

    /// <summary>
    /// Busca transações associadas ao ID de uma pessoa específica, validando sua existência.
    /// </summary>
    /// <param name="personId">ID da pessoa proprietária das transações.</param>
    /// <exception cref="KeyNotFoundException">Lançada caso a pessoa não exista.</exception>
    public async Task<IEnumerable<Transaction>> GetByPersonIdAsync(int personId)
    {
        // Valida se a pessoa existe no banco de dados.
        var person = await _personRepository.GetByIdAsync(personId);
        if (person == null)
            throw new KeyNotFoundException("Pessoa não encontrada no sistema.");

        // Se existe, busca as transações.
        return await _transactionRepository.GetByPersonIdAsync(personId);
    }
    
    /// <summary>
    /// Remove uma transação após validar sua existência.
    /// </summary>
    /// <param name="id">ID da transação a ser removida.</param>
    /// <exception cref="KeyNotFoundException">Lançada se a transação não existir</exception>
    public async Task DeleteAsync(int id)
    {
        // Reutiliza o GetByIdAsync para garantir que a transação existe.
        var transaction = await GetByIdAsync(id);
        await _transactionRepository.DeleteAsync(transaction);
    }
}