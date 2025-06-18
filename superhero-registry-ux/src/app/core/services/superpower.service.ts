import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Superhero, Superpoder} from '../types/hero';
import {Observable} from 'rxjs';
import {Response} from '../types/response';

@Injectable({
  providedIn: 'root'
})
export class SuperpowerService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private readonly httpClient: HttpClient) {}

  getAllSuperpowers(): Observable<Response<Superpoder>> {
    return this.httpClient.get<Response<Superpoder>>(`${this.apiUrl}/super-poder`);
  }
}
