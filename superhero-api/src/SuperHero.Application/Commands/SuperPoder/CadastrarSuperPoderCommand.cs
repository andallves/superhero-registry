using SuperHero.Application.DTO;

namespace SuperHero.Application.Commands.SuperPoder;

public class CadastrarSuperPoderCommand : BaseCommand<SuperPoderDto>
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; } = null;
}