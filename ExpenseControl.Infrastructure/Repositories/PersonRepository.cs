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
    /// <returns>A entidade encontrada ou null caso nao exista.</returns>>
    public async Task<Person?> GetByIdAsync(int id)
    {
        return await _context.People.FindAsync(id);
    }

    /// <summary>
    /// Busca todas as pessoas registradas no banco de dados.
    /// </summary>
    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        // ToListAsync garante que a consulta seja executada de forma assíncrona
        return await _context.People.ToListAsync();
    }

    /// <summary>
    /// Remove uma pessoa do banco de dados e confirma a transação.
    /// </summary>
    public async Task DeleteAsync(Person person)
    {
        _context.People.Remove(person);
        await _context.SaveChangesAsync();
    }
}