using ExpenseControl.API.DTOs.Person;
using ExpenseControl.Core.Entities;
using ExpenseControl.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseControl.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }
    
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PersonResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PersonResponse>> Create(CreatePersonRequest request)
    {
        // Instancia a entidade do tipo Person com os dados do construtor do DTO.
        var person = new Person
        {
            Name = request.Name,
            Age = request.Age
        };

        // Persiste os dados no banco através do repositório.
        await _personService.CreateAsync(person);
        
        // Mapeia o resultado para o DTO de resposta.
        var response = new PersonResponse(person.Id, person.Name, person.Age);

        // Retorna o status 201 (Created).
        // O CreatedAtAction retorna a URL para buscar a pessoa criada.
        return CreatedAtAction(nameof(Get), new { id = person.Id }, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PersonResponse>> Get(int id)
    {
        // Busca a pessoa no banco de dados através do identificador único.
        var person = await _personService.GetByIdAsync(id);
        
        // Verifica se a pessoa existe no banco de dados.
        if (person == null)
        {
            return NotFound();
        }
        
        // Mapeia o resultado para um DTO de resposta.
        var response = new PersonResponse(person.Id, person.Name, person.Age);

        return Ok(response);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PersonResponse>))]
    public async Task<ActionResult<IEnumerable<PersonResponse>>> GetAll()
    {
        // Busca a entidade 
        var people = await _personService.GetAllAsync();
        
        // Usa o LINQ para transformar cada Person em PersonResponse.
        var response = people.Select(p => new PersonResponse(p.Id, p.Name, p.Age));
        
        // Retorna a lista com os DTOs de resposta.
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {

        // Busca a pessoa pelo identificador único.
        var person = await _personService.GetByIdAsync(id);

        // Verifica se a pessoa existe no banco de dados.
        if (person == null)
        {
            return NotFound();
        }
        
        // Deleta a pessoa do banco de dados.
        await _personService.DeleteAsync(id);
        return NoContent();
    }
}