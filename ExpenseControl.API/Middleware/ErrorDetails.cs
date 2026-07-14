namespace ExpenseControl.API.Middleware;

/// <summary>
/// Representa a estrutura de erro padronizada que será retornada pela API.
/// </summary>
public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
}