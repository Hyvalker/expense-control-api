using ExpenseControl.Core.Entities;
using ExpenseControl.Core.Interfaces;
using ExpenseControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControl.Infrastructure.Repositories;

/// <summary>
/// Repositório responsável pelas operações de persistência e leitura de dados da entidade Person.
/// Implementa a interface IPersonRepository utilizando o Entity Framework Core.
/// </summary>
public class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adiciona uma nova pessoa ao contexto e persiste as alterações no banco de dados.
    /// </summary>
    public async Task AddAsync(Person person)
    {
        await _context.People.AddAsync(person);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Busca uma pessoa específica no banco de dados através de seu identificador único.
    /// </summary>
    public async Task<Person?> GetByIdAsync(int id)
    {
        return await _context.People.FindAsync(id);
    }

    /// <summary>
    /// Busca todas as pessoas registradas no banco de dados e às retorna em uma lista.
    /// </summary>
    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _context.People.ToListAsync();
    }

    /// <summary>
    /// Deleta uma pessoa do banco de dados
    /// </summary>
    public async Task DeleteAsync(Person person)
    {
        _context.People.Remove(person);
        await _context.SaveChangesAsync();
    }
}