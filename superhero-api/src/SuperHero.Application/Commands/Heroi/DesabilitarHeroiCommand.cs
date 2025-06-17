using SuperHero.Application.DTO;

namespace SuperHero.Application.Commands.Heroi;

public class DesabilitarHeroiCommand : BaseCommand<HeroiDto>
{
    public int HeroiId { get; set; }
}