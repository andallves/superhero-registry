import {Superhero} from './hero';

export interface Response {
  haMaisPaginas: boolean;
  haPaginas: boolean;
  hePrimeiraPagina: boolean;
  heUltimaPagina: boolean;
  paginaAtual: number;
  resultado: Superhero[];
  tamanhoDaPagina: number;
  totalDePaginas: number;
  totalDeResultados: number;
  ultimaPagina: number;
}
