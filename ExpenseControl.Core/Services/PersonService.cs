using ExpenseControl.Core.Entities;
using ExpenseControl.Core.Interfaces;

namespace ExpenseControl.Core.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    
    public  PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Person> CreateAsync(Person person)
    {
        await _personRepository.AddAsync(person);
        return person;
    }

    public async Task<Person?> GetByIdAsync(int id)
    {
        return await _personRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _personRepository.GetAllAsync();
    }

    public async Task DeleteAsync(int id)
    {   
        var person = await _personRepository.GetByIdAsync(id);
        if (person != null)
        {
            await _personRepository.DeleteAsync(person);
        }
    }
}