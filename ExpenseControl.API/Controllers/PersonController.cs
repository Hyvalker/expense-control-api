using ExpenseControl.API.DTOs.Person;
using ExpenseControl.Core.Entities;
using ExpenseControl.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseControl.API.Controllers;
/// <summary>
/// Controlador para gerenciamento de pessoas no sistema.
/// </summary>
[ApiController]
[Route("api/people")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }
    
    /// <summary>
    /// Cadastra uma nova pessoa no sistema.
    /// </summary>
    /// <response code="201">Pessoa Criada com sucesso.</response>
    /// <response code="400">Dados inválidos enviados na requisição.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PersonResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PersonResponse>> Create(CreatePersonRequest request)
    {
        
        var person = new Person
        {
            Name = request.Name,
            Age = request.Age
        };
        
        await _personService.CreateAsync(person);
        
        var response = new PersonResponse(person.Id, person.Name, person.Age);
        
        return CreatedAtAction(nameof(Get), new { id = person.Id }, response);
    }

    /// <summary>
    /// Busca os detalhes de uma pessoa através de seu identificador único.
    /// </summary>
    /// <response code="200">Retorna a pessoa solicitada</response>
    /// <response code="404">Pessoa não encontrada</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PersonResponse>> Get(int id)
    {
        var person = await _personService.GetByIdAsync(id);
        var response = new PersonResponse(person.Id, person.Name, person.Age);

        return Ok(response);
    }
    /// <summary>
    /// Lista todas as pessoas cadastradas.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PersonResponse>))]
    public async Task<ActionResult<IEnumerable<PersonResponse>>> GetAll()
    {
        var people = await _personService.GetAllAsync();
        
        // Usa o LINQ para transformar cada Person em PersonResponse.
        var response = people.Select(p => new PersonResponse(p.Id, p.Name, p.Age));
        
        return Ok(response);
    }

    /// <summary>
    /// Remove uma pessoa do sistema.
    /// </summary>
    /// <response code="204">Pessoa removida com sucesso.</response>
    /// <response code="404">Pessoa não encontrada</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        // O service valida se a pessoa existe no banco de dados. Caso exista, aqui ela é excluída.
        await _personService.DeleteAsync(id);
        return NoContent();
    }
}