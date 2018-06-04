import { Injectable } from '@angular/core';
import { hero } from '../classes/hero'
import { HEROES } from '../mock-hero-list';
import { Observable, of } from 'rxjs';
import { MessageService } from './message.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HeroService {

  url: string = 'api/heroes';
  constructor(private httpClient: HttpClient, private messageService: MessageService) { }

  getHeroes(): Observable<hero []>{
    this.messageService.add('HeroService: fetched heroes');
    // return of(HEROES);
    return this.httpClient.get<hero[]>(this.url);
  }

  getHero(id: number){
    this.messageService.add(`HeroService: fetched hero id=${id}` );
    //return of (HEROES.find(hero => hero.id === id));
    return this.httpClient.get<hero>(`${this.url}/${id}`);
  }

  updateHero(hero: hero): Observable<any> {
    this.messageService.add(`HeroService: updated hero id=${hero.id} to name=${hero.name}`);
    return this.httpClient.put(this.url, hero);
  }
}
