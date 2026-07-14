namespace ExpenseControl.API.DTOs.Person;

/// <summary>
/// Resumo financeiro de uma pessoa para exibição na interface.
/// </summary>
/// <param name="Id">ID da pessoa.</param>
/// <param name="Name">Nome da pessoa.</param>
/// <param name="TotalIncome">Receita total dessa pessoa.</param>
/// <param name="TotalExpense">Despesa total dessa pessoa.</param>
/// <param name="Balance">Saldo final dessa pessoa.</param>
public record PersonSummaryResponse(int Id, string Name, decimal TotalIncome, decimal TotalExpense, decimal Balance);