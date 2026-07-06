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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configuração de relacionamento entre Transaction e Person
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Person) // Uma transação tem uma pessoa
            .WithMany(p => p.Transactions) // Uma pessoa tem várias transações
            .HasForeignKey(t => t.PersonId) // A chave estrangeira é a PersonID
            .OnDelete(DeleteBehavior.Cascade); // Ao deletar uma pessoa, são deletadas em cascata todas as transações vinculadas à essa pessoa 
    }
}