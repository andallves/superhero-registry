using SuperHero.Domain.Entities;

namespace SuperHero.Application.DTO;

public class SuperPoderDto : SoftDeleteEntity
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; } = null;
}