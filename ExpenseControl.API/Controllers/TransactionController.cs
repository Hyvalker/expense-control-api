using ExpenseControl.API.DTOs.Transaction;
using ExpenseControl.Core.Entities;
using ExpenseControl.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
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
            transaction.PersonId);

        // Retorna o status 201 (Created).
        // O CreatedAtAction retorna a URL para buscar a transação criada.
        return CreatedAtAction(nameof(Get), new { id = transaction.Id }, response);
    }

    [HttpGet("{id}")]
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
            transaction.PersonId);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionResponse>>> GetAll()
    {
        // Busca a entidade 
        var transactions = await _transactionService.GetAllAsync();
        
        // Usa o LINQ para transformar cada Transaction em TransactionResponse.
        var response = transactions.Select(t => new TransactionResponse(
            t.Id, t.Description, t.Amount, t.Type, t.PersonId));
        
        // Retorna a lista com os DTOs de resposta.
        return Ok(response);
    }

    [HttpGet("report")]
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
    
}