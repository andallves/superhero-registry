namespace SuperHero.Domain.Entities.Hero;

public class SuperPoder
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; } = null;
    public ICollection<HeroiSuperPoder> HeroisSuperPoderes { get; set; } = new List<HeroiSuperPoder>();
}