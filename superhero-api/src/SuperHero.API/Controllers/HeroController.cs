using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperHero.Application.Commands.Heroi;
using SuperHero.Application.DTO;
using SuperHero.Application.Queries.Heroi;
using SuperHero.Domain.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace SuperHero.API.Controllers;

[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class HeroController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    [MapToApiVersion("1.0")]
    [SwaggerOperation(Summary = "Obtem um heroi existente", Tags = ["Heroi"])]
    [ProducesResponseType(typeof(PagedResult<HeroiDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async ValueTask<IActionResult> Obter([FromQuery] BuscarHeroisQuery query, CancellationToken cancellationToken)
    {
        return await SendQueryAsync(query, cancellationToken);
    }
    
    [HttpGet("{id:int}")]
    [MapToApiVersion("1.0")]
    [SwaggerOperation(Summary = "Obtem um heroi existente por id", Tags = ["Heroi"])]
    [ProducesResponseType(typeof(HeroiDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async ValueTask<IActionResult> Obter([FromRoute] int id, CancellationToken cancellationToken)
    {
        return await SendQueryAsync(new ObterHeroiPorIdQuery { Id = id }, cancellationToken);
    }

    [HttpPost]
    [MapToApiVersion("1.0")]
    [SwaggerOperation(Summary = "Cadastra um novo heroi", Tags = ["Heroi"])]
    [ProducesResponseType(typeof(HeroiDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async ValueTask<IActionResult> Cadastrar([FromBody] CadastrarHeroiCommand command, CancellationToken cancellationToken)
    {
        return await SendCommandAsync(command, cancellationToken);
    }
    
    [HttpPut("{id:int}")]
    [MapToApiVersion("1.0")]
    [SwaggerOperation(Summary = "Atualiza um heroi existente", Tags = ["Heroi"])]
    [ProducesResponseType(typeof(HeroiDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> Atualizar([FromRoute] int id, [FromForm] AlterarHeroiCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        return await SendCommandAsync(command, cancellationToken);
    }
    
    [HttpDelete("{id:int}")]
    [MapToApiVersion("1.0")]
    [SwaggerOperation(Summary = "Arquiva um heroi existente", Tags = ["Heroi"])]
    [ProducesResponseType(typeof(HeroiDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> Desativar([FromRoute] int id, CancellationToken cancellationToken)
    {
        return await SendCommandAsync(new DesabilitarHeroiCommand { HeroiId = id }, cancellationToken);
    }
    
    [HttpPatch("{id:int}/habilitar")]
    [MapToApiVersion("1.0")]
    [SwaggerOperation(Summary = "Habilita um heroi existente", Tags = ["Heroi"])]
    [ProducesResponseType(typeof(HeroiDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async ValueTask<IActionResult> Habilitar([FromRoute] int id, CancellationToken cancellationToken)
    {
        return await SendCommandAsync(new HabilitarHeroiCommand { HeroiId = id }, cancellationToken);
    }
}