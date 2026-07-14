using ExpenseControl.API.DTOs.Transaction;
using ExpenseControl.Core.Entities;
using ExpenseControl.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseControl.API.Controllers;

/// <summary>
/// Controlador para gerenciamento de transações financeiras.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    /// <summary>
    /// Cria uma nova transação.
    /// </summary>
    /// <response code="201">Transação criada com sucesso.</response>
    /// <response code="400">Dados inválidos ou violação de regra de negócio.</response>
    /// <response code="404">Pessoa não encontrada/</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TransactionResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransactionResponse>> Create(CreateTransactionRequest request)
    {
        var transaction = new Transaction
        {
            Description = request.Description,
            Amount = request.Amount,
            Type = request.Type,
            PersonId = request.PersonId,
        };
        
        await _transactionService.CreateAsync(transaction);


        // Mapeia o resultado para o DTO de resposta.
        var response = new TransactionResponse(
            transaction.Id,
            transaction.Description,
            transaction.Amount,
            transaction.Type,
            transaction.PersonId,
            transaction.Person?.Name ?? "Sem Nome");

        
        // O CreatedAtAction retorna a URL para buscar a transação criada.
        return CreatedAtAction(nameof(Get), new { id = transaction.Id }, response);
    }

    /// <summary>
    /// Busca uma transação pelo identificador único.
    /// </summary>
    /// <param name="id">ID da transação.</param>
    /// <returns>A transação buscada ou nulo.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransactionResponse>> Get(int id)
    {
        var transaction = await _transactionService.GetByIdAsync(id);
        
        // Mapeia o resultado para um DTO de resposta.
        var response = new TransactionResponse(
            transaction.Id,
            transaction.Description,
            transaction.Amount,
            transaction.Type,
            transaction.PersonId,
            transaction.Person?.Name ?? "Sem Nome");

        return Ok(response);
    }

    /// <summary>
    /// Retorna o relatório completo de receitas e despesas.
    /// </summary>
    [HttpGet("report")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReportResponse))]
    public async Task<ActionResult<ReportResponse>> GetReport()
    {
        var model = await _transactionService.GetReportAsync();

        // Mapeia o modelo interno para o contrato da API
        var response = new ReportResponse(
            model.PeopleReport.Select(p => new PersonReport(p.Name, p.TotalIncome, p.TotalExpense, p.Balance)),
            model.TotalGeneralIncome,
            model.TotalGeneralExpense,
            model.TotalGeneralBalance
        );

        return Ok(response);
    }

    /// <summary>
    /// Lista todas as transações, com opção de filtro por pessoa.
    /// </summary>
    /// <param name="personId">ID opcional da pessoa para filtrar.</param>
    /// <returns>Lista com todas transações do sistema ou todas transações de uma pessoa específica.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransactionResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<TransactionResponse>>> GetAll([FromQuery] int? personId)
    {
        
            var transactions = personId.HasValue
                ? await _transactionService.GetByPersonIdAsync(personId.Value)
                : await _transactionService.GetAllAsync();
            
            var response = transactions.Select(t => new TransactionResponse(
                t.Id,
                t.Description,
                t.Amount,
                t.Type,
                t.PersonId,
                t.Person?.Name ?? "Sem Nome"));
            return Ok(response);
        
    }
    
    /// <summary>
    /// Remove uma transação do banco de dados.
    /// </summary>
    /// <param name="id">ID da transação a ser removida.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _transactionService.DeleteAsync(id);
        return NoContent();
    }
}