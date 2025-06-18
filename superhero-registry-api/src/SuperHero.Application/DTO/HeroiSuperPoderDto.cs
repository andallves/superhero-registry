using SuperHero.Domain.Entities.Hero;

namespace SuperHero.Application.DTO;

public class HeroiSuperPoderDto
{
    public int Id { get; set; }
    public int HeroiId { get; set; }
    public int SuperPoderId { get; set; }
    
    public static HeroiSuperPoderDto From(HeroiSuperPoder heroiSuperPoder)
    {
        return new HeroiSuperPoderDto
        {
            Id = heroiSuperPoder.Id,
            HeroiId = heroiSuperPoder.HeroiId,
            SuperPoderId = heroiSuperPoder.SuperPoderId
        };
    }

    public static List<HeroiSuperPoderDto> From(List<HeroiSuperPoder> heroiSuperPoderes)
    {
        var listaHeroisSuperPoderes = new List<HeroiSuperPoderDto>();

        foreach (var heroiSuperPoder in heroiSuperPoderes)
        {
            listaHeroisSuperPoderes.Add(From(heroiSuperPoder));
        }

        return listaHeroisSuperPoderes;
    }

}