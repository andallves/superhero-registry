import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'superpoderNome',
  standalone: true
})
export class SuperpoderNomePipe implements PipeTransform {

  transform(superPoderId: number, listaPoderes: { id: number, nome: string }[]): string {
    const poder = listaPoderes.find(p => p.id === superPoderId);
    return poder ? poder.nome : 'Poder desconhecido';
  }

}
