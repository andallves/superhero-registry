import {Component, EventEmitter, Input, Output} from '@angular/core';
import {NgOptimizedImage} from '@angular/common';
import {Superhero} from '../../../core/types/hero';

@Component({
  selector: 'app-hero-card',
  standalone: true,
  imports: [
    NgOptimizedImage
  ],
  templateUrl: './hero-card.component.html',
  styleUrl: './hero-card.component.scss'
})
export class HeroCardComponent {
  @Input({ alias: 'hero', required: true }) hero!: Superhero;
  @Output('deleteHeroClicked') deleteHeroClicked = new EventEmitter();
  @Output('editHeroClicked') editHeroClicked = new EventEmitter();

  handleClickEdit() {
    this.editHeroClicked.emit();
  }

  handleClickDelete() {
    this.deleteHeroClicked.emit();
  }
}
