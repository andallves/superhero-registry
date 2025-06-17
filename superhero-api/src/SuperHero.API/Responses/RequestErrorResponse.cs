namespace SuperHero.API.Responses;

public abstract class RequestErrorResponse
{
    public string Mensagem { get; set; } = string.Empty;
    public int Status { get; set; }
    public string[] Erros { get; set; } = [];
}