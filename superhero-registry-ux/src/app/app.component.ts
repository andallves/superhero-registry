import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {NgOptimizedImage} from '@angular/common';
import {NavbarComponent} from './core/template/layout/navbar/navbar.component';
import {FilterComponent} from './core/template/layout/filter/filter.component';
import {HeroiCardComponent} from './shared/components/heroi-card/heroi-card.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgOptimizedImage, NavbarComponent, FilterComponent, HeroiCardComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  showModal = false;
  editingHero: any = null;
  heroes = [];

  addHeroButton() {
    this.editingHero = null;
    this.showModal = true;
  }

  editHero(hero: any) {
    this.editingHero = hero;
    this.showModal = true;
  }

  deleteHero(hero: any) {
    this.heroes = this.heroes.filter(h => h.id !== hero.id);
  }

  handleSave(hero: any) {
    if (hero.id) {
      this.heroes = this.heroes.map(h => h.id === hero.id ? hero : h);
    } else {
      hero.id = Date.now(); // simulação de ID
      this.heroes.push(hero);
    }
  }

  handleCloseModal() {
    this.showModal = false;
  }
}
}
