namespace SuperHero.API.Responses;

public sealed class BadRequestErrorResponse : RequestErrorResponse
{
    public BadRequestErrorResponse(string[] erros, int status = 400, string mensagem = "")
    {
        Erros = erros;
        Status = status;
        Mensagem = string.IsNullOrEmpty(mensagem) ? "Ocorreram um ou mais erros de validação" : mensagem;
    }
}