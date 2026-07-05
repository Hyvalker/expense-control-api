using Microsoft.EntityFrameworkCore;
using ExpenseControl.Core.Entities;

namespace ExpenseControl.Infrastructure.Data;

public class AppDbContext : DbContext
{
    /// <summary>
    /// Contexto principal do banco de dados, responsável pela injeção 
    /// de dependência das configurações e acesso às tabelas.
    /// </summary>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Person> People { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}