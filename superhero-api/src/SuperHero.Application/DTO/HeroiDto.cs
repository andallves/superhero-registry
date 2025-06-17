namespace SuperHero.Application.DTO;

public class HeroiDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string NomeHeroi { get; set; } = string.Empty;
    public DateTime? DataNascimento { get; set; } = null;
    public float Altura { get; set; }
    public float Peso { get; set; }
    public List<HeroiSuperPoderDto> HeroisSuperPoderes { get; set; } = new List<HeroiSuperPoderDto>();
}