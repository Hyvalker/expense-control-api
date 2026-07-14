using ExpenseControl.Core.Entities;
using ExpenseControl.Core.Interfaces;

namespace ExpenseControl.Core.Services;


/// <summary>
///  Serviço responsável pelas regras de negócio da entidade Person.
/// Realiza as validações antes de delegar a persistência ao repositório.
/// </summary>
public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    
    public  PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    /// <summary>
    /// Cria uma nova pessoa após validar os campos obrigatórios.
    /// </summary>
    /// <param name="person">A entidade pessoa a ser criada.</param>
    /// <returns>A pessoa criada com o identificador único atribuído pelo banco de dados.</returns>>
    /// <exception cref="InvalidOperationException">Lançada se o nome da pessoa estiver vazio ou nulo.</exception>
    public async Task<Person> CreateAsync(Person person)
    {
        if (string.IsNullOrWhiteSpace(person.Name))
            throw new InvalidOperationException("O nome é obrigatório.");
        
        await _personRepository.AddAsync(person);
        return person;
    }

    /// <summary>
    /// Busca os detalhes de uma pessoa escpecífica através de seu identificador.
    /// </summary>
    /// <param name="id">O identificador único da pessoa</param>
    /// <returns>A instância da entidade Person encontrada</returns>
    /// <exception cref="KeyNotFoundException">Lançada caso não exista uma pessoa cadastrada com o identificador fornecido.</exception>
    public async Task<Person> GetByIdAsync(int id)
    {
        var person = await _personRepository.GetByIdAsync(id);
        
        if (person == null)
            throw new KeyNotFoundException($"Pessoa com ID {id} não encontrada.");

        return person;
    }

    /// <summary>
    /// Retorna uma lista contendo todas as pessoas cadastradas no sistema.
    /// </summary>
    /// <returns>Uma coleção enumerável de objetos Person</returns>
    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _personRepository.GetAllAsync();
    }

    /// <summary>
    /// Remove uma pessoa do banco de dados.
    /// </summary>
    /// <param name="id">O identificador únido da pessoa a ser removida.</param>
    /// <remarks>
    /// Este método realiza uma busca prévia para garantir que a pessoa existe no banco.
    /// A deleção pode disparar uma remoção em cascata (Cascade Delete) nas transações vinculadas à pessoa.
    /// </remarks>
    /// <exception cref="KeyNotFoundException">Lançada caso o identificador informado não seja encontrado no banco.</exception>
    public async Task DeleteAsync(int id)
    {   
        var person = await GetByIdAsync(id);
        await _personRepository.DeleteAsync(person);
        
    }
}