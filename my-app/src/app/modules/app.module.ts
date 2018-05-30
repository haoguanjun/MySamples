import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from '../components/app/app.component';
import { HeroListComponent } from '../components/hero-list/hero-list.component';
import { HeroDetailComponent } from '../components/hero-detail/hero-detail.component';
import { MessagesComponent } from '../components/messages/messages.component';
import { AppRoutingModule } from './/app-routing.module';
import { DashboardComponent } from '../components/dashboard/dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    HeroListComponent,
    HeroDetailComponent,
    MessagesComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
