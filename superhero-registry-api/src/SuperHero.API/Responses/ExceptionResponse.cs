using System.Net;
using System.Text.Json.Serialization;

namespace SuperHero.API.Responses;

public class ExceptionResponse
{
    [JsonPropertyOrder(order: 1)]
    public string Title { get; set; } = null!;
    [JsonPropertyOrder(order: 2)]
    public int Status { get; set; }
    
    public ExceptionResponse()
    {
        Title = "Ops, ocorreu um erro no servidor";
        Status = (int)HttpStatusCode.InternalServerError;
    }

    public ExceptionResponse(string title) : this()
    {
        Title = title;
    }

}