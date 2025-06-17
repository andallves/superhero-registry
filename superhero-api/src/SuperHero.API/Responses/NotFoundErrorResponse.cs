namespace SuperHero.API.Responses;

public sealed class NotFoundErrorResponse : RequestErrorResponse
{
    public NotFoundErrorResponse(string mensagem, int status = 404, string[]? erros = null)
    {
        Mensagem = mensagem;
        Status = status;
        Erros = erros ?? [];
    }
}