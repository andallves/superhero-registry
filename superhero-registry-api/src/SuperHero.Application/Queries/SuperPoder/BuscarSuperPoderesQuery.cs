using SuperHero.Application.DTO;
using SuperHero.Domain.ValueObjects;

namespace SuperHero.Application.Queries.SuperPoder;

public class BuscarSuperPoderesQuery : BasePagedQuery<Domain.Entities.Hero.SuperPoder, SuperPoderDto>
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    
    public override void AplicarFiltro(ref IQueryable<Domain.Entities.Hero.SuperPoder> query)
    {
        if (!string.IsNullOrEmpty(Nome))
        {
            query = query.Where(u => u.Nome.Contains(Nome));
        }
        
        if (!string.IsNullOrEmpty(Descricao))
        {
            query = query.Where(u => u.Descricao != null && u.Descricao.Contains(Descricao));
        }
    }
    
    public override void AplicarOrdenacao(ref IQueryable<Domain.Entities.Hero.SuperPoder> query)
    {
        if (OrdenarAscendente)
        {
            query = OrdenarPor.ToLower() switch
            {
                "nome" => query.OrderBy(x => x.Nome),
                "descricao" => query.OrderBy(x => x.Descricao),
                _ => query.OrderBy(x => x.Nome)
            };
            return;
        }
        
        query = OrdenarPor.ToLower() switch
        {
            "nome" => query.OrderByDescending(x => x.Nome),
            "descricao" => query.OrderByDescending(x => x.Descricao),
            _ => query.OrderByDescending(x => x.Nome)
        };
    }
}
