using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseControl.Core.Enums;

namespace ExpenseControl.Core.Entities;

/// <summary>
/// Representa uma transação financeira (receita ou despesa).
/// </summary>
public class Transaction
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)] // Limite de caracteres para a descrição da transação.
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [Column(TypeName = "decimal(18,2)")] // Define a precisão de casas decimais para o banco de dados
    public decimal Amount { get; set; }
    
    [Required]
    public TransactionType Type { get; set; }
    
    [Required]
    public DateTime Date { get; set; }

    /// <summary>
    /// Foreign Key para o relacionamento com a classe Person
    /// </summary>
    [ForeignKey(nameof(Person))]
    public int PersonId { get; set; }

    /// <summary>
    /// Propriedade de navegação para o relacionamento das entidades pelo EntityFramework
    /// </summary>
    public Person Person { get; set; } = null!;
}