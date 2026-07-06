namespace ExpenseControl.Core.Models;

public record PersonReportModel(string Name, decimal TotalIncome, decimal TotalExpense, decimal Balance);

public record ReportModel(
    IEnumerable<PersonReportModel> PeopleReport,
    decimal TotalGeneralIncome,
    decimal TotalGeneralExpense,
    decimal TotalGeneralBalance
    );