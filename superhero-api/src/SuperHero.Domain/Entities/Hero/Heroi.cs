namespace SuperHero.Domain.Entities.Hero;

public class Heroi : SoftDeleteEntity
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string NomeHeroi { get; set; } = string.Empty;
    public DateTime? DataNascimento { get; set; } = null;
    public float Altura { get; set; }
    public float Peso { get; set; }
    public List<HeroiSuperPoder> HeroisSuperPoderes { get; set; } = [];
    
    public void Habilitar() => Desativado = false;
    public void Desativar() => Desativado = true;
}