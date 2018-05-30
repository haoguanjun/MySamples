import { Component, OnInit } from '@angular/core';
import { HeroListComponent } from '../hero-list/hero-list.component'
import { HeroService } from '../../services/hero.service';
import { hero } from '../../classes/hero';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  heroes: hero[];

  constructor(private heroService: HeroService) { }

  ngOnInit() {
    this.getHeroes();
  }

  getHeroes(): void{
    this.heroService.getHeroes().subscribe(heroes => this.heroes = heroes);
  }
}
