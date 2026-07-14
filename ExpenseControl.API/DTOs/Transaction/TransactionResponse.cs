using ExpenseControl.Core.Enums;

namespace ExpenseControl.API.DTOs.Transaction;
/// <summary>
/// Objeto de resposta para consulta de transações.
/// </summary>
/// <param name="Id">ID da transação.</param>
/// <param name="Description">Descrição da transação.</param>
/// <param name="Amount">Valor da transação.</param>
/// <param name="Type">Tipo de transação (receita ou despesa)</param>
/// <param name="PersonId">ID da pessoa vinculada à transação.</param>
/// <param name="PersonName">Nome da pessoa vinculada (opcional, adicionado aqui para facilitar a exibição no frontend).</param>
public record TransactionResponse(int Id, string Description, decimal Amount, TransactionType Type, int PersonId, string? PersonName);