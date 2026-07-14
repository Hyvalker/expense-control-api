namespace ExpenseControl.Core.Enums;

/// <summary>
/// Define os tipos possíveis de transações financeiras.
/// </summary>
public enum TransactionType
{
    /// <summary>
    /// Representa uma entrada de dinheiro (receita).
    /// </summary>
    Income = 0,
    
    /// <summary>
    /// Representa uma saída de dinheiro (despesa).
    /// </summary>
    Expense = 1
}