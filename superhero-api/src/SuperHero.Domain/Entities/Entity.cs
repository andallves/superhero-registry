namespace SuperHero.Domain.Entities;

public abstract class Entity : ITracking
{
    public int Id { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
}

public abstract class SoftDeleteEntity : Entity, ISoftDelete
{
    public bool Desativado { get; set; } = false;
}