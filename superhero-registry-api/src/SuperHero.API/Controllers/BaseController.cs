using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperHero.API.Responses;
using SuperHero.Application.Commands;
using SuperHero.Application.Queries;
using SuperHero.Domain.ValueObjects;

namespace SuperHero.API.Controllers;

[ApiController]
[ExcludeFromCodeCoverage]
public abstract class BaseController(IMediator mediator) : ControllerBase
{
    protected readonly IMediator Mediator = mediator;

    protected async Task<IActionResult> SendCommandAsync<T>(BaseCommand<T> request,
        CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return result.Sucesso
            ? Ok(result.Resultado)
            : ErroResponse(result);
    }

    protected IActionResult ErroResponse<T>(CustomResult<T> result)
    {
        return result.ErrorType switch
        {
            EResultErrorType.NotFound => Problem(title: result.Mensagem, statusCode: StatusCodes.Status404NotFound),
            EResultErrorType.ServerError => Problem(title: result.Mensagem, detail: string.Join('\n', result.Erros),
                statusCode: StatusCodes.Status500InternalServerError),
            EResultErrorType.ServiceError => Problem(title: result.Mensagem, detail: string.Join('\n', result.Erros),
                statusCode: StatusCodes.Status503ServiceUnavailable),
            EResultErrorType.Validation or _ => BadRequest(new BadRequestErrorResponse(result.Erros.ToArray(),
                mensagem: result.Mensagem))
        };
    }
    
    protected async Task<IActionResult> SendQueryAsync<T>(BaseQuery<T> request, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(request, cancellationToken);
        
        return result.Sucesso
            ? Ok(result.Resultado)
            : NotFound(new NotFoundErrorResponse(result.Mensagem, erros: result.Erros.ToArray()));
    }
    
    protected async Task<IActionResult> SendQueryAsync<T, TY>(BasePagedQuery<T, TY> request, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}