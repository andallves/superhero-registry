using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperHero.Application.Commands.SuperPoder;
using SuperHero.Application.DTO;
using SuperHero.Domain.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace SuperHero.API.Controllers;

[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class SuperPoderController(IMediator mediator) : BaseController(mediator)
{
    
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