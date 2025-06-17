namespace SuperHero.Domain.Entities;

public interface ISoftDelete
{
    bool Desativado { get; set; }
}