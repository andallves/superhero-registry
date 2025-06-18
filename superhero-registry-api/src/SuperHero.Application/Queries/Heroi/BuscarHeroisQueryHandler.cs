using MediatR;
using Microsoft.EntityFrameworkCore;
using SuperHero.Application.DTO;
using SuperHero.Domain.ValueObjects;
using SuperHero.Infra.Context;
using SuperHero.Infra.Database;
using SuperHero.Infra.Extensions;

namespace SuperHero.Application.Queries.Heroi;

public class BuscarHeroisQueryHandler : IRequestHandler<BuscarHeroisQuery, PagedResult<HeroiDto>>
{
    private readonly IRepository<SuperHeroDbContext> _repository;
    
    public BuscarHeroisQueryHandler(IRepository<SuperHeroDbContext> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<HeroiDto>> Handle(BuscarHeroisQuery request, CancellationToken cancellationToken)
    {
        var paged = await _repository
            .GetQueryable<Domain.Entities.Hero.Heroi>()
            .Include(x => x.HeroisSuperPoderes)
            .AplicarFiltro(request)
            .AplicarOrdenacao(request)
            .Select(x => new HeroiDto
            {
                Id = x.Id,
                Nome = x.Nome,
                NomeHeroi = x.NomeHeroi,
                Altura = x.Altura,
                Peso = x.Peso,
                HeroisSuperPoderes = HeroiSuperPoderDto.From(x.HeroisSuperPoderes),
            })
            .PagedAsync(request, cancellationToken);
        
        return paged;
    }
}