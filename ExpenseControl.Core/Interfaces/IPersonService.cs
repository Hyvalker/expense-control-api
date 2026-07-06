using ExpenseControl.Core.Entities;

namespace ExpenseControl.Core.Interfaces;
/// <summary>
/// Contrato de serviço para a entidade Person.
/// Orquestra as regras de negócio e utiliza o repositório.
/// </summary>
public interface IPersonService
{
    /// <summary>
    /// Cria uma nova pessoa no banco de dados.
    /// </summary>
    Task<Person> CreateAsync(Person person);
    
    /// <summary>
    /// Busca uma pessoa pelo identificador único.
    /// </summary>
    Task<Person?> GetByIdAsync(int id);
    
    /// <summary>
    /// Lista todas as pessoas cadastradas.
    /// </summary>
    Task<IEnumerable<Person>> GetAllAsync();
    
    /// <summary>
    /// Remove uma pessoa e, consequentemente, todas suas transações cadastradas (através do Cascade Delete).
    /// </summary>
    Task DeleteAsync(int id);
}