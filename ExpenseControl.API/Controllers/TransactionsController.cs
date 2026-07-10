using ExpenseControl.API.DTOs.Transaction;
using ExpenseControl.Core.Entities;
using ExpenseControl.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

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

        try
        {
            // Persiste os dados no banco através do repositório.
            await _transactionService.CreateAsync(transaction);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        // Mapeia o resultado para o DTO de resposta.
        var response = new TransactionResponse(
            transaction.Id, 
            transaction.Description, 
            transaction.Amount,
            transaction.Type, 
            transaction.PersonId,
            transaction.Person?.Name ?? "Sem Nome");

        // Retorna o status 201 (Created).
        // O CreatedAtAction retorna a URL para buscar a transação criada.
        return CreatedAtAction(nameof(Get), new { id = transaction.Id }, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransactionResponse>> Get(int id)
    {
        // Busca a transação no banco de dados através do identificador único.
        var transaction = await _transactionService.GetByIdAsync(id);
        
        // Verifica se a transação existe no banco de dados.
        if (transaction == null)
        {
            return NotFound();
        }
        
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
    
    [HttpGet("report")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReportResponse))]
    public async Task<ActionResult<ReportResponse>> GetReport()
    {
        // Chama a regra de negócios para o relatório através de TransactionService
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransactionResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<TransactionResponse>>> GetAll([FromQuery] int? personId)
    {
        try
        {
            IEnumerable<Transaction> transactions;

            if (personId.HasValue)
            {
                transactions = await _transactionService.GetByPersonIdAsync(personId.Value);
            }
            else
            {
                transactions = await _transactionService.GetAllAsync();
            }

            var response = transactions.Select(t => new TransactionResponse(
                t.Id,
                t.Description,
                t.Amount,
                t.Type,
                t.PersonId,
                t.Person?.Name ?? "Sem Nome"));
            return Ok(response);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        
    }
}