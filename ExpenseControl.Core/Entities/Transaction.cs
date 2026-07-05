using ExpenseControl.Core.Enums;

namespace ExpenseControl.Core.Entities;

public class Transaction
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }

    /// <summary>
    /// Foreign Key para o relacionamento com a classe Person
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// Propriedade de navegação para o relacionamento das entidades pelo EntityFramework
    /// </summary>
    public Person Person { get; set; } = null!;
}