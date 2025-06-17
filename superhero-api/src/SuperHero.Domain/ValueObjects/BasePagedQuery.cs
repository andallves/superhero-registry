using MediatR;
using System.Diagnostics.CodeAnalysis;
using SuperHero.Domain.ValueObjects;

namespace SuperHero.Domain.ValueObjects;

[ExcludeFromCodeCoverage]
public abstract class BasePagedQuery<TSearchEntity, TResultDto> : PagedSearch, IRequest<PagedResult<TResultDto>>
{
    public virtual void AplicarFiltro(ref IQueryable<TSearchEntity> query)
    { }

    public virtual void AplicarOrdenacao(ref IQueryable<TSearchEntity> query)
    { }
    
}