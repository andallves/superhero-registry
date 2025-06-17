using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperHero.Application.Commands.SuperPoder;
using SuperHero.Application.DTO;
using SuperHero.Application.Queries.SuperPoder;
using SuperHero.Domain.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace SuperHero.API.Controllers;

[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class SuperPoderController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    [MapToApiVersion("1.0")]
    [SwaggerOperation(Summary = "Busca superpoderes existentes", Tags = ["SuperPoder"])]
    [ProducesResponseType(typeof(PagedResult<HeroiDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async ValueTask<IActionResult> Obter([FromQuery] BuscarSuperPoderesQuery query, CancellationToken cancellationToken)
    {
        return await SendQueryAsync(query, cancellationToken);
    }
    
    [HttpPost]
    [MapToApiVersion("1.0")]
    [SwaggerOperation(Summary = "Cadastra um novo superpoder", Tags = ["SuperPoder"])]
    [ProducesResponseType(typeof(SuperPoderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async ValueTask<IActionResult> Cadastrar([FromBody] CadastrarSuperPoderCommand command, CancellationToken cancellationToken)
    {
        return await SendCommandAsync(command, cancellationToken);
    }
}