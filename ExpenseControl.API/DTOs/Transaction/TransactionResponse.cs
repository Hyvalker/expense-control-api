using ExpenseControl.Core.Enums;

namespace ExpenseControl.API.DTOs.Transaction;

public record TransactionResponse(int Id, string Description, decimal Amount, TransactionType Type, int PersonId, string? PersonName);