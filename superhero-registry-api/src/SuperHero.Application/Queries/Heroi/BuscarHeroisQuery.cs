using SuperHero.Application.DTO;
using SuperHero.Domain.Entities.Hero;
using SuperHero.Domain.ValueObjects;

namespace SuperHero.Application.Queries.Heroi;

public class BuscarHeroisQuery : BasePagedQuery<Domain.Entities.Hero.Heroi, HeroiDto>
{
    public string? Nome { get; set; }
    public string? NomeHeroi { get; set; }
    public int? SuperPoder { get; set; }
    
    public override void AplicarFiltro(ref IQueryable<Domain.Entities.Hero.Heroi> query)
    {
        if (!string.IsNullOrEmpty(Nome))
        {
            query = query.Where(u => u.Nome.Contains(Nome));
        }
        
        if (!string.IsNullOrEmpty(NomeHeroi))
        {
            query = query.Where(u => u.NomeHeroi.Contains(NomeHeroi));
        }
        
        if (SuperPoder is not null)
        {
            query = query.Where(u => u.HeroisSuperPoderes.Exists(x => x.SuperPoderId == SuperPoder));
        }
        
    }
    
    public override void AplicarOrdenacao(ref IQueryable<Domain.Entities.Hero.Heroi> query)
    {
        if (OrdenarAscendente)
        {
            query = OrdenarPor.ToLower() switch
            {
                "nome" => query.OrderBy(x => x.Nome),
                "nomeheroi" => query.OrderBy(x => x.NomeHeroi),
                _ => query.OrderBy(x => x.Nome)
            };
            return;
        }
        
        query = OrdenarPor.ToLower() switch
        {
            "nome" => query.OrderByDescending(x => x.Nome),
            "nomeheroi" => query.OrderByDescending(x => x.NomeHeroi),
            _ => query.OrderByDescending(x => x.Nome)
        };
    }
}