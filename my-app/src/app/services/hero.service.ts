import { Injectable } from '@angular/core';
import { hero } from '../classes/hero'
import { HEROES } from '../mock-hero-list';
import { Observable, of } from 'rxjs';
import { MessageService } from './message.service';

@Injectable({
  providedIn: 'root'
})
export class HeroService {

  constructor(private messageService: MessageService) { }

  getHeroes(): Observable<hero []>{
    this.messageService.add('HeroService: fetched heroes');
    return of(HEROES);
  }

  getHero(id: number){
    this.messageService.add(`HeroService: fetched hero id=${id}` );
    return of (HEROES.find(hero => hero.id === id));
  }
}
