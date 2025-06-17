using AutoMapper;

namespace SuperHero.Domain.ValueObjects;

public sealed class PagedResult<T>
{
    public List<T> Resultado { get; set; } = [];
    public int PaginaAtual { get; set; }
    public int TamanhoDaPagina { get; set; }
    public int TotalDePaginas { get; set; }
    public int TotalDeResultados { get; set; }

    public int UltimaPagina => TotalDePaginas;
    public bool HaPaginas => TotalDePaginas > 0;
    public bool HePrimeiraPagina => PaginaAtual == 1;
    public bool HeUltimaPagina => PaginaAtual == TotalDePaginas;
    public bool HaMaisPaginas => TotalDePaginas > PaginaAtual;

    public PagedResult<TOut> ToDtoResult<TOut>(IMapper mapper)
    {
        return new PagedResult<TOut>
        {
            Resultado = mapper.Map<List<TOut>>(Resultado),
            PaginaAtual = PaginaAtual,
            TamanhoDaPagina = TamanhoDaPagina,
            TotalDePaginas = TotalDePaginas,
            TotalDeResultados = TotalDeResultados
        };
    }
}