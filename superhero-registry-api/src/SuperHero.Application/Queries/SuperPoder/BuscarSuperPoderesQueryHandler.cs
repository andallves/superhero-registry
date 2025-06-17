using MediatR;
using SuperHero.Application.DTO;
using SuperHero.Domain.ValueObjects;
using SuperHero.Infra.Context;
using SuperHero.Infra.Database;
using SuperHero.Infra.Extensions;

namespace SuperHero.Application.Queries.SuperPoder;

public class BuscarSuperPoderesQueryHandler : IRequestHandler<BuscarSuperPoderesQuery, PagedResult<SuperPoderDto>>
{
    private readonly IRepository<SuperHeroDbContext> _repository;
    
    public BuscarSuperPoderesQueryHandler(IRepository<SuperHeroDbContext> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<SuperPoderDto>> Handle(BuscarSuperPoderesQuery request, CancellationToken cancellationToken)
    {
        var paged = await _repository
            .GetQueryable<Domain.Entities.Hero.SuperPoder>()
            .AplicarFiltro(request)
            .AplicarOrdenacao(request)
            .Select(x => new SuperPoderDto
            {
                Id = x.Id,
                Nome = x.Nome,
                Descricao = x.Descricao,
            })
            .PagedAsync(request, cancellationToken);
        
        return paged;
    }
}