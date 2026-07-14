using ExpenseControl.Core.Entities;

namespace ExpenseControl.Core.Interfaces;
/// <summary>
/// Contrato de serviço para a entidade Person.
/// Orquestra as regras de negócio e utiliza o repositório para a persistência.
/// </summary>
public interface IPersonService
{
    /// <summary>
    /// Cria uma nova pessoa no banco de dados, aplicando as validações de regra de negócios.
    /// </summary>
    /// <param name="person">Objeto contendo os dados da pessoa a ser cadastrada.</param>
    /// <returns>A pessoa criada com seu identificador gerado.</returns>
    Task<Person> CreateAsync(Person person);
    
    /// <summary>
    /// Busca uma pessoa pelo identificador único.
    /// </summary>
    /// <param name="id">Identificador único da pessoa.</param>
    /// <returns>A entidade Person caso encontrada; caso não encontre, pode retornar nulo ou lançar uma exceção.</returns>>
    Task<Person> GetByIdAsync(int id);
    
    /// <summary>
    /// Lista todas as pessoas cadastradas.
    /// </summary>
    Task<IEnumerable<Person>> GetAllAsync();
    
    /// <summary>
    /// Remove uma pessoa e todas as suas transações cadastradas.
    /// A deleção é tratada em cascata pelo banco de dados.
    /// </summary>
    /// <param name="id">Identificador da pessoa a ser removida</param>
    Task DeleteAsync(int id);
}