namespace SuperHero.Domain.ValueObjects;

public abstract class PagedSearch
{
    public int ItensPorPagina { get; set; } = 10;
    public int Pagina { get; set; } = 1;

    public string OrdenarPor { get; set; } = "Id";
    public bool OrdenarAscendente { get; set; } = true;
}