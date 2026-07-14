using System.ComponentModel.DataAnnotations;

namespace ExpenseControl.API.DTOs.Person;

/// <summary>
/// Objeto de requisição para cadastro de uma nova pessoa.
/// </summary>
/// <param name="Name">Nome completo da pessoa. Campo obrigatório.</param>
/// <param name="Age">Idade da pessoa. Deve ser um número positivo.</param>
public record CreatePersonRequest(
    [Required] 
    [StringLength(100)] 
    string Name,
    
    [Range(0, 150)] 
    int Age
);