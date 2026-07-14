namespace ExpenseControl.API.DTOs.Person;

/// <summary>
/// Objeto de resposta para consulta de pessoas.
/// </summary>
/// <param name="Id">ID da pessoa.</param>
/// <param name="Name">Nome da pessoa.</param>
/// <param name="Age">Idade da pessoa.</param>
public record PersonResponse(int Id, string Name, int Age);