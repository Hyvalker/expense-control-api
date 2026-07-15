using System.ComponentModel.DataAnnotations;
using ExpenseControl.Core.Enums;

namespace ExpenseControl.API.DTOs.Transaction;

/// <summary>
/// Objeto de requisição para criar uma nova transação financeira.
/// </summary>
public record CreateTransactionRequest(
    [Required(ErrorMessage = "A descrição da transação é obrigatória.")]
    [StringLength(200)]
    string Description,
    
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor da transação deve ser maior que zero.")]
    decimal Amount, 
    
    [Required]
    TransactionType Type, 
    
    [Required]
    int PersonId);