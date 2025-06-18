import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Superhero} from '../types/hero';
import {Observable} from 'rxjs';
import {Response} from '../types/response';

@Injectable({
  providedIn: 'root'
})
export class HeroService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private readonly httpClient: HttpClient) {}

  saveHero(heroData: Superhero): Observable<Superhero> {
    return this.httpClient.post<Superhero>(
      `${this.apiUrl}/heroi`,
      heroData
    );
  }

  getAllHeroes(): Observable<Response<Superhero>> {
    return this.httpClient.get<Response<Superhero>>(`${this.apiUrl}/heroi`);
  }

  updateHero(
    heroId: number,
    heroData: Partial<Superhero>
  ): Observable<Superhero> {
    return this.httpClient.put<Superhero>(
      `${this.apiUrl}/heroi/${heroId}`,
      heroData
    );
  }

  deleteHero(heroId: number): Observable<Superhero> {
    return this.httpClient.delete<Superhero>(`${this.apiUrl}/heroi/desativar/${heroId}`);
  }
}
