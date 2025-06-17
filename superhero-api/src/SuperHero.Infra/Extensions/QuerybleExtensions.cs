using Microsoft.EntityFrameworkCore;
using SuperHero.Domain.ValueObjects;

namespace SuperHero.Infra.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> AplicarFiltro<T, TY>(this IQueryable<T> queryable, BasePagedQuery<T, TY> obj)
    {
        obj.AplicarFiltro(ref queryable);
        return queryable;
    }
    
    public static IQueryable<T> AplicarOrdenacao<T, TY>(this IQueryable<T> queryable, BasePagedQuery<T, TY> obj)
    {
        obj.AplicarOrdenacao(ref queryable);
        return queryable;
    }
    
    public static async Task<PagedResult<T>> PagedAsync<T>(this IQueryable<T> query, PagedSearch pagedSearch, CancellationToken cancellationToken = default)
    {
        var quantidade = await query.CountAsync(cancellationToken);
        var resultado = await query
            .Skip((pagedSearch.Pagina - 1) * pagedSearch.ItensPorPagina)
            .Take(pagedSearch.ItensPorPagina)
            .ToListAsync(cancellationToken);
        
        return new PagedResult<T>
        {
            Resultado = resultado,
            PaginaAtual = pagedSearch.Pagina,
            TamanhoDaPagina = resultado.Count,
            TotalDeResultados = quantidade,
            TotalDePaginas = (int)Math.Ceiling((double)quantidade / pagedSearch.ItensPorPagina)
        };
    }
}