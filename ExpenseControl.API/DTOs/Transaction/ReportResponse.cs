namespace ExpenseControl.API.DTOs.Transaction;

/// <summary>
/// Dados detalhados de uma pessoa para o relatório financeiro da API.
/// </summary>
/// <param name="Name">Nome da pessoa.</param>
/// <param name="TotalIncome">Receita total da pessoa.</param>
/// <param name="TotalExpense">Despesa total da pessoa</param>
/// <param name="Balance">Saldo final da pessoa.</param>
public record PersonReport(string Name, decimal TotalIncome, decimal TotalExpense, decimal Balance);

/// <summary>
/// Resposta final do relatório financeiro completo da API.
/// </summary>
/// <param name="TotalGeneralIncome">Receita total do sistema.</param>
/// <param name="TotalGeneralExpense">Despesa total do sistema.</param>
/// <param name="TotalGeneralBalance">Saldo final do sistema.</param>
public record ReportResponse(
    IEnumerable<PersonReport> PeopleReports,
    decimal TotalGeneralIncome,
    decimal TotalGeneralExpense,
    decimal TotalGeneralBalance
    );