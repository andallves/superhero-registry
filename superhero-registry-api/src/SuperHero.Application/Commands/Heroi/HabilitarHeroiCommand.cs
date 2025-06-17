using SuperHero.Application.DTO;

namespace SuperHero.Application.Commands.Heroi;

public class HabilitarHeroiCommand : BaseCommand<HeroiDto>
{
    public int HeroiId { get; set; }
}