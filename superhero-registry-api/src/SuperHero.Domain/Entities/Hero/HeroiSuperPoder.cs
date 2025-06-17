namespace SuperHero.Domain.Entities.Hero;

public class HeroiSuperPoder
{
    public int Id { get; set; }
    public int HeroiId { get; set; }
    public Heroi Heroi { get; set; } = null!;
    public int SuperPoderId { get; set; }
    public SuperPoder SuperPoder { get; set; } = null!;
}