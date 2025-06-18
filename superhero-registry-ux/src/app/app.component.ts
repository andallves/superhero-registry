import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {NgFor, NgOptimizedImage} from '@angular/common';
import {NavbarComponent} from './core/template/layout/navbar/navbar.component';
import {FilterComponent} from './core/template/layout/filter/filter.component';
import {HeroCardComponent} from './shared/components/hero-card/hero-card.component';
import {HeroService} from './core/services/hero.service';
import {Superhero, Superpoder} from './core/types/hero';
import {ResultComponent} from './shared/components/result/result.component';
import {Response} from './core/types/response';
import {HeroModalComponent} from './shared/components/hero-modal/hero-modal.component';
import {SuperpowerService} from './core/services/superpower.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgOptimizedImage, NavbarComponent, FilterComponent, HeroCardComponent, NgFor, ResultComponent, HeroModalComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  showModal = false;
  isLoading = false;
  editingHero: any = null;
  heroes: Superhero[] = [];
  allPowers: Superpoder[] = [];

  constructor(private readonly heroService: HeroService, private readonly superpowerService: SuperpowerService) {}

  ngOnInit(): void {
    this.getAllHeroes();
  }

  private getAllHeroes(): void {
    const active = false;
    this.heroService.getAllHeroes().subscribe({
      next: (data: Response<Superhero>) => {
        console.log(data.resultado);
        this.heroes = data.resultado;
      },
      error: (erro) => console.log(erro),
    });

    this.superpowerService.getAllSuperpowers().subscribe({
      next: (data: Response<Superpoder>) => {
        this.allPowers = data.resultado;
      }
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

  handleSave(hero: Superhero) {
    this.isLoading = true;
    this.heroService.saveHero(hero).subscribe( {
      next: () => {
        console.log("Salvo")
      },
      error: (error) => {},
      complete: () => {
        this.showModal = false;
        this.isLoading = false;
      }
    })
  }

  handleUpdate(hero: Superhero) {
    this.isLoading = true;
    this.heroService.updateHero(hero.id, hero).subscribe( {
      next: () => {

      },
      error: (error) => {},
      complete: () => {
        this.showModal = false;
        this.isLoading = false;
      }
    })
  }

  handleCloseModal() {
    this.showModal = false;
  }
}
