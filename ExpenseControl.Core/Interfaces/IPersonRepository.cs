using ExpenseControl.Core.Entities;

namespace ExpenseControl.Core.Interfaces;

/// <summary>
/// Define os contratos para operações de persistência da entidade Pessoa.
/// </summary>
public interface IPersonRepository
{
    /// <summary>
    /// Adiciona uma nova pessoa ao sistema.
    /// </summary>
    Task AddAsync(Person person);

    /// <summary>
    /// Busca uma pessoa pelo seu identificador único (Id).
    /// </summary>
    Task<Person> GetByIdAsync(int id);

    /// <summary>
    /// Retorna todas as pessoas cadastradas no sistema.
    /// </summary>
    Task<IEnumerable<Person>> GetAllAsync();

    /// <summary>
    /// Remove uma pessoa cadastrada no sistema
    /// Essa operação também removerá todas as transações vinculadas à pessoa.
    /// </summary>
    Task DeleteAsync(Person person);
}