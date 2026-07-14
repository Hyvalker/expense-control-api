using System.ComponentModel.DataAnnotations;

namespace ExpenseControl.Core.Entities;

/// <summary>
/// Representa uma pessoa cadastrada no sistema
/// </summary>
public class Person
{
    [Key] //Define o campo como chave primária
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)] // Define o limite de caracteres para o nome no banco de dados.
    public string Name { get; set; } = string.Empty;
    
    [Range(0, 150)] // Evite uso de idades absurdas na camada de persistência.
    public int Age { get; set; }
    
    /// <summary>
    /// Lista de transações que estarão vinculada a essa pessoa.
    /// </summary>
    public List<Transaction> Transactions { get; set; } = new ();
}