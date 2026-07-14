namespace ExpenseControl.Core.Models;

/// <summary>
/// Modelo de negócio com os totais de uma pessoa específica.
/// </summary>
/// <param name="Name">Nome da pessoa.</param>
/// <param name="TotalIncome">Receita total dessa pessoa.</param>
/// <param name="TotalExpense">Despesa totail dessa pessoa.</param>
/// <param name="Balance">Saldo final dessa pessoa.</param>
public record PersonReportModel(string Name, decimal TotalIncome, decimal TotalExpense, decimal Balance);

/// <summary>
/// Modelo de negócio que consolida os relatórios de todas as pessoas e os totais gerais do sistema.
/// </summary>
/// <param name="TotalGeneralIncome">Receita total do sistema.</param>
/// <param name="TotalGeneralExpense">Despesa total do sistema.</param>
/// <param name="TotalGeneralBalance">Saldo final do sistema.</param>
public record ReportModel(
    IEnumerable<PersonReportModel> PeopleReport,
    decimal TotalGeneralIncome,
    decimal TotalGeneralExpense,
    decimal TotalGeneralBalance
    );