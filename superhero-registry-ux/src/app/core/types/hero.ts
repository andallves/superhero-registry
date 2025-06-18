export interface Superhero {
  id: number;
  nome: string;
  nomeHeroi: string;
  heroisSuperPoderes: HeroisSuperPoderes[];
  altura: number;
  peso: number;
}

export interface HeroisSuperPoderes {
  id?: number;
  heroiId?: number;
  superPoderId: number;
}

export interface Superpoder {
  id: number;
  nome: string;
  descricao: string;
}

export interface SuperheroForm {
  nome: string;
  nomeHeroi: string;
  heroisSuperPoderes: Superpoder[];
  altura: number;
  peso: number;
}
