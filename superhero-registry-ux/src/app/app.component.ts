import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {NgFor, NgOptimizedImage} from '@angular/common';
import {NavbarComponent} from './core/template/layout/navbar/navbar.component';
import {FilterComponent} from './core/template/layout/filter/filter.component';
import {HeroCardComponent} from './shared/components/hero-card/hero-card.component';
import {HeroService} from './core/services/hero.service';
import {Superhero} from './core/types/hero';
import {ResultComponent} from './shared/components/result/result.component';
import {Response} from './core/types/response';
import {HeroModalComponent} from './shared/components/hero-modal/hero-modal.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgOptimizedImage, NavbarComponent, FilterComponent, HeroCardComponent, NgFor, ResultComponent, HeroModalComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  showModal = false;
  editingHero: any = null;
  heroes: Superhero[] = [];

  constructor(private readonly heroService: HeroService) {}

  ngOnInit(): void {
    this.getAllHeroes();
  }

  private getAllHeroes(): void {
    const active = false;
    this.heroService.getAllHeroes().subscribe({
      next: (data: Response) => {
        console.log(data.resultado);
        this.heroes = data.resultado;
      },
      error: (erro) => console.log(erro),
    });
  }


  addButtonClick() {
    this.editingHero = null;
    this.showModal = true;
  }

  editButtonClick(hero: Superhero) {
    this.editingHero = hero;
    this.showModal = true;
  }

  deleteButtonClick(hero: Superhero) {
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
