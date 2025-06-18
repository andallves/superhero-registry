import {Component, EventEmitter, Input, Output} from '@angular/core';
import {NgForOf, NgOptimizedImage} from '@angular/common';
import {Superhero, Superpoder} from '../../../core/types/hero';
import {SuperpoderNomePipe} from '../../../core/pipes/superpoder-nome.pipe';

@Component({
  selector: 'app-hero-card',
  standalone: true,
  imports: [
    NgOptimizedImage,
    NgForOf,
    SuperpoderNomePipe
  ],
  templateUrl: './hero-card.component.html',
  styleUrl: './hero-card.component.scss'
})
export class HeroCardComponent {
  @Input({ alias: 'hero', required: true }) hero!: Superhero;
  @Input() allPowers: Superpoder[] = [];
  @Output('deleteHeroClicked') deleteHeroClicked = new EventEmitter();
  @Output('editHeroClicked') editHeroClicked = new EventEmitter();

  handleClickEdit() {
    this.editHeroClicked.emit();
  }

  handleClickDelete() {
    this.deleteHeroClicked.emit();
  }
}
