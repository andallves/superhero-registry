using SuperHero.Application.DTO;

namespace SuperHero.Application.Queries.Heroi;

public class ObterHeroiPorIdQuery: BaseQuery<HeroiDto>
{
    public long Id { get; set; }
}