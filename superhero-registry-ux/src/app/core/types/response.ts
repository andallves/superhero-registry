export interface Response<T> {
  haMaisPaginas: boolean;
  haPaginas: boolean;
  hePrimeiraPagina: boolean;
  heUltimaPagina: boolean;
  paginaAtual: number;
  resultado: T[];
  tamanhoDaPagina: number;
  totalDePaginas: number;
  totalDeResultados: number;
  ultimaPagina: number;
}
