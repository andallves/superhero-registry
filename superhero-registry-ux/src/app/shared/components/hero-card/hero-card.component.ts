import {Component, EventEmitter, Input, Output} from '@angular/core';
import {NgForOf, NgOptimizedImage} from '@angular/common';
import {Superhero, Superpoder} from '../../../core/types/hero';
import {SuperpoderNomePipe} from '../../../core/pipes/superpoder-nome.pipe';
import {SweetAlert2Module} from '@sweetalert2/ngx-sweetalert2';

@Component({
  selector: 'app-hero-card',
  standalone: true,
  imports: [
    NgOptimizedImage,
    NgForOf,
    SuperpoderNomePipe,
    SweetAlert2Module,
  ],
  templateUrl: './hero-card.component.html',
  styleUrl: './hero-card.component.scss'
})
export class HeroCardComponent {
  @Input({ alias: 'hero', required: true }) hero!: Superhero;
  @Input() allPowers: Superpoder[] = [];
  @Output('deleteHeroClicked') deleteHeroClicked = new EventEmitter<number>();
  @Output('editHeroClicked') editHeroClicked = new EventEmitter<number>();

  handleClickEdit(id: number) {
    this.editHeroClicked.emit(id);
  }

  handleClickDelete(id: number) {
    this.deleteHeroClicked.emit(id);
  }
}
