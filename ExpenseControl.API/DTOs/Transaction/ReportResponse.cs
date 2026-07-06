namespace ExpenseControl.API.DTOs.Transaction;

public record PersonReport(string Name, decimal TotalIncome, decimal TotalExpense, decimal Balance);

public record ReportResponse(
    IEnumerable<PersonReport> PeopleReports,
    decimal TotalGeneralIncome,
    decimal TotalGeneralExpense,
    decimal TotalGeneralBalance
    );