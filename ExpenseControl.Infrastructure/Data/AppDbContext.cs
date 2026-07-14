using Microsoft.EntityFrameworkCore;
using ExpenseControl.Core.Entities;

namespace ExpenseControl.Infrastructure.Data;

/// <summary>
/// Contexto principal do banco de dados, responsável pelo mapeamento das entidades e acesso às tabelas.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Inicializa uma nova instância do contexto com as configurações injetadas.
    /// </summary>
    /// <param name="options">Opções de configuração do DbContext.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    /// <summary>
    /// Tabela de pessoas cadastradas no sistema.
    /// </summary>
    public DbSet<Person> People { get; set; }
    
    /// <summary>
    /// Tabela de transações financeiras.
    /// </summary>
    public DbSet<Transaction> Transactions { get; set; }

    /// <summary>
    /// Configura o modelo de dados e os relacionamentos entre as entidades.
    /// </summary>
    /// <param name="modelBuilder">Construtor do modelo utilizado para configurar o mapeamento.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configuração de relacionamento entre Transaction e Person
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Person) // Uma transação tem uma pessoa
            .WithMany(p => p.Transactions) // Uma pessoa tem várias transações
            .HasForeignKey(t => t.PersonId) // A chave estrangeira é a PersonID
            .OnDelete(DeleteBehavior.Cascade); // Ao remover uma pessoa, são deletadas em cascata todas as transações vinculadas à essa pessoa 
    }
}