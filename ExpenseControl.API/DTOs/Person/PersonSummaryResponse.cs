namespace ExpenseControl.API.DTOs.Person;

public record PersonSummaryResponse(int Id, string Name, decimal TotalIncome, decimal TotalExpense, decimal Balance);