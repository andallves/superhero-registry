namespace SuperHero.Domain.ValueObjects;

public class CustomResult<T>
{
    public T? Resultado { get; set; } = default(T);
    public bool Sucesso { get; set; }

    public string Mensagem { get; set; } = null!;
    public List<string> Erros { get; set; } = [];
    public EResultErrorType? ErrorType { get; set; }

    public static CustomResult<T> SuccessResult(T? resultado = default(T), string mensagem = "")
    {
        return new CustomResult<T>
        {
            Resultado = resultado,
            Sucesso = true,
            Mensagem = mensagem
        };
    }
    
    public static CustomResult<T> ErrorResult(string mensagem, List<string>? erros = null, EResultErrorType errorType = EResultErrorType.Validation)
    {
        return new CustomResult<T>
        {
            Sucesso = false,
            Mensagem = mensagem,
            Erros = erros ?? [],
            ErrorType = errorType,
        };
    }
}

public enum EResultErrorType
{
    Validation = 400,
    NotFound = 404,
    ServerError = 500,
    ServiceError = 503,
}