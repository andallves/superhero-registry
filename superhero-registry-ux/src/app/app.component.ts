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
import Swal from 'sweetalert2';
import {LoadingComponent} from './shared/components/loading/loading.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgOptimizedImage, NavbarComponent, FilterComponent, HeroCardComponent, NgFor, ResultComponent, HeroModalComponent, LoadingComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  showModal = false;
  isLoading = false;
  editingHero: Superhero | null = null;
  heroes: Superhero[] = [];
  allPowers: Superpoder[] = [];

  constructor(private readonly heroService: HeroService, private readonly superpowerService: SuperpowerService) {
    this.isLoading = true;
  }

  ngOnInit(): void {
    this.getAllHeroes();
  }

  getAllHeroes(): void {
    this.heroService.getAllHeroes().subscribe({
      next: (data: Response<Superhero>) => {
        console.log(data.resultado)
        this.heroes = data.resultado;
      },
      error: (erro) => console.log(erro),
      complete: () => this.isLoading = false,
    });

    this.isLoading = false;

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

  handleSave(hero: Superhero) {
    this.isLoading = true;
    Swal.fire({
      icon: "question",
      title: 'Tem certeza',
      text: 'Tem certeza que deseja criar este herói?',
      confirmButtonText: 'Sim, Criar!',
      showCancelButton: true,
      cancelButtonText: 'Cancelar',
      focusConfirm: true
    }).then((response) => {
      if (response.isConfirmed) {
        this.heroService.saveHero(hero).subscribe({
          next: () => {
            Swal.fire('Criado!', 'Herói criado com sucesso!', 'success');
            this.getAllHeroes();
          },
          error: (error) => {
            Swal.fire({
              icon: "error",
              title: 'Ops',
              text: 'Ocorreu um erro.',
            })
          },
          complete: () => {
            this.showModal = false;
            this.isLoading = false;
          }
        })
      }
    })
  }

  handleUpdate(hero: Superhero) {
    this.isLoading = true;
    Swal.fire({
      icon: "question",
      title: 'Tem certeza',
      text: 'Deseja salvar as alterações feitas neste herói?',
      confirmButtonText: 'Sim, Salvar!',
      showCancelButton: true,
      cancelButtonText: 'Cancelar',
      focusConfirm: true
    }).then((response) => {
      if (response.isConfirmed) {
        this.heroService.updateHero(hero.id, hero).subscribe({
          next: () => {
            Swal.fire('Atualizado!', 'Herói atualizado com sucesso!', 'success');
            this.getAllHeroes();
          },
          error: (error) => {
            Swal.fire({
              icon: "error",
              title: 'Ops',
              text: 'Ocorreu um erro.',
            })
            console.log(error)
            this.isLoading = false;
          },
          complete: () => {
            this.showModal = false;
            this.isLoading = false;
          }
        })
      }
    })
  }

  handleDelete(id: number) {
    this.isLoading = true;
    Swal.fire({
      icon: "question",
      title: 'Tem certeza',
      text: 'Tem certeza que deseja excluir este herói? Esta ação não poderá ser desfeita.',
      confirmButtonText: 'Sim, Excluir!',
      showCancelButton: true,
      cancelButtonText: 'Cancelar',
      focusConfirm: true
    }).then((response) => {
      if (response.isConfirmed) {
        this.heroService.deleteHero(id).subscribe({
          next: () => {
            Swal.fire('Excluído!', 'Herói excluído com sucesso.', 'success');
            this.heroes = this.heroes.filter(hero => hero.id != id);
          },
          error: (error) => {
            Swal.fire({
              icon: "error",
              title: 'Ops',
              text: 'Ocorreu um erro.',
            })
          },
          complete: () => {
            this.showModal = false;
            this.isLoading = false;

          }
        })
      }
    })
  }

  handleSearchById(id: number) {
    if (!id) return;
    console.log(id);
    this.heroService.getById(id).subscribe({
      next: (hero) => {
        this.heroes = [hero];
      },
      error: (erro) => {
        console.log(erro)
        Swal.fire({
          icon: "error",
          title: 'Ops',
          text: 'Heroi não encontrado.',
        })
      }
    });
  }

  handleCloseModal() {
    this.showModal = false;
  }
}
