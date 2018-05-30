import { Component, OnInit } from '@angular/core';
import { hero } from '../../classes/hero';
import { HeroDetailComponent } from '../hero-detail/hero-detail.component';
import { HeroService } from '../../services/hero.service';

@Component({
  selector: 'app-hero-list',
  templateUrl: './hero-list.component.html',
  styleUrls: ['./hero-list.component.css']
})
export class HeroListComponent implements OnInit {

  heroes: hero[];
  selectedHero: hero;

  constructor(private heroService: HeroService) { 
  }

  ngOnInit() {
    this.getHeroes();
  }

  onSelect(hero: hero): void {
    this.selectedHero = hero;
  }

  onRefresh(heroes: hero[]): void {
    this.getHeroes();
  }

  getHeroes(): void{
    this.heroService.getHeroes().subscribe(heroes => this.heroes = heroes);
  }
}
