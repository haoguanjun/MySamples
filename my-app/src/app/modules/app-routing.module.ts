import { NgModule } from '@angular/core';
import { RouterModule, Router, Routes } from '@angular/router';
import { HeroListComponent } from '../components/hero-list/hero-list.component';
import { DashboardComponent } from '../components/dashboard/dashboard.component';
import { HeroDetailComponent } from '../components/hero-detail/hero-detail.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'heroes', component: HeroListComponent },
  { path: 'heroes/:id', component: HeroDetailComponent},
  { path: 'dashboard', component: DashboardComponent }  
];

@NgModule({
  exports:[
    RouterModule
  ],
  imports: [
    RouterModule.forRoot(routes)
  ]
})
export class AppRoutingModule { 
  
}
