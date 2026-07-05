using ExpenseControl.Core.Enums;

namespace ExpenseControl.API.DTOs.Transaction;

public record CreateTransactionRequest(string Description, decimal Amount, TransactionType Type, int PersonId);